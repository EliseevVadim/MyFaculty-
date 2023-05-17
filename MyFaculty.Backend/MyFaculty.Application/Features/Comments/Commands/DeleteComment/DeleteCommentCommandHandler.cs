using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, CommentViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public DeleteCommentCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentViewModel> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            Comment deletingComment = await _context.Comments
                .Include(comment => comment.Post)
                .FirstOrDefaultAsync(comment => comment.Id == request.Id, cancellationToken);
            if (deletingComment == null)
                throw new EntityNotFoundException(nameof(Comment), request.Id);
            if (deletingComment.Post is InfoPost)
            {
                InfoPost infoPost = await _context.InfoPosts
                    .Include(infoPost => infoPost.OwningInformationPublic)
                        .ThenInclude(infoPublic => infoPublic.BlockedUsers)
                    .Include(infoPost => infoPost.OwningInformationPublic)
                        .ThenInclude(infoPublic => infoPublic.Moderators)
                    .Include(infoPost => infoPost.OwningStudyClub)
                        .ThenInclude(club => club.Moderators)
                    .FirstOrDefaultAsync(infoPost => infoPost.Id == deletingComment.Post.Id);
                if (infoPost.OwningInformationPublic != null && infoPost.OwningInformationPublic.IsBanned)
                    throw new EntityNotFoundException(nameof(InfoPost), infoPost.Id);
                if (!infoPost.CommentsAllowed)
                    throw new DestructiveActionException("Вы не можете работать с комментариями к этой записи, поскольку в ней они запрещены комментарии");
                if (infoPost.OwningInformationPublic != null
                    && infoPost
                        .OwningInformationPublic
                        .BlockedUsers
                        .Any(user => user.Id == request.IssuerId))
                    throw new DestructiveActionException("Вы не можете удалить комментарий к этой записи, так как были заблокированы " +
                        "в сообществе, содержащем ее");
                if (deletingComment.AuthorId != request.IssuerId &&
                        ((infoPost.OwningInformationPublic != null && !infoPost.OwningInformationPublic.Moderators.Any(user => user.Id == request.IssuerId)) ||
                        (infoPost.OwningStudyClub != null && !infoPost.OwningStudyClub.Moderators.Any(user => user.Id == request.IssuerId))))
                {
                    throw new UnauthorizedActionException("Данное действие Вам запрещено.");
                }
            }
            else if (deletingComment.Post is ClubTask)
            {
                ClubTask task = await _context.ClubTasks
                    .Include(clubTask => clubTask.OwningStudyClub)
                        .ThenInclude(club => club.Moderators)
                    .FirstOrDefaultAsync(clubTask => clubTask.Id == deletingComment.Post.Id);
                if (deletingComment.AuthorId != request.IssuerId &&
                        !task.OwningStudyClub.Moderators.Any(user => user.Id == request.IssuerId))
                    throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            }
            _context.Comments.Remove(deletingComment);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CommentViewModel>(deletingComment);
        }
    }
}
