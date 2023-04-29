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

namespace MyFaculty.Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, CommentViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCommentCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentViewModel> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment updatingComment = await _context.Comments
                .Include(comment => comment.Post)
                .FirstOrDefaultAsync(comment => comment.Id == request.Id, cancellationToken);
            if (updatingComment == null)
                throw new EntityNotFoundException(nameof(Comment), request.Id);
            if (updatingComment.AuthorId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            if (updatingComment.Post is InfoPost)
            {
                InfoPost infoPost = await _context.InfoPosts
                    .Include(infoPost => infoPost.OwningInformationPublic)
                        .ThenInclude(infoPublic => infoPublic.BlockedUsers)
                    .FirstOrDefaultAsync(infoPost => infoPost.Id == updatingComment.Post.Id);
                if (infoPost.OwningInformationPublic != null && infoPost.OwningInformationPublic.IsBanned)
                    throw new EntityNotFoundException(nameof(InfoPost), infoPost.Id);
                if (!infoPost.CommentsAllowed)
                    throw new DestructiveActionException("Вы не можете работать с комментариями к этой записи, поскольку в ней запрещены комментарии");
                if (infoPost.OwningInformationPublic != null
                    && infoPost
                        .OwningInformationPublic
                        .BlockedUsers
                        .Select(user => user.Id)
                        .Contains(request.IssuerId))
                    throw new DestructiveActionException("Вы не можете изменить комментарий к этой записи, так как были заблокированы " +
                        "в сообществе, содержащем ее");
            }
            if (request.ParentCommentId != null)
            {
                Comment newParent = await _context.Comments.FirstOrDefaultAsync(comment => comment.Id == request.ParentCommentId, cancellationToken);
                if (newParent == null)
                    throw new EntityNotFoundException(nameof(Comment), request.ParentCommentId);
                if (newParent.PostId != updatingComment.PostId)
                    throw new DestructiveActionException("Вы не можете ответить на комментарий, принадлежащий другой записи");
                if (newParent.Id == updatingComment.Id)
                    throw new DestructiveActionException("Вы не можете ответить на этот же комментарий");
                if (newParent.Created > updatingComment.Created)
                    throw new DestructiveActionException("Вы не можете ответить на комментарий, написанный позже Вашего");
            }
            updatingComment.TextContent = request.TextContent;
            updatingComment.Attachments = request.Attachments;
            updatingComment.ParentCommentId = request.ParentCommentId;
            updatingComment.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CommentViewModel>(updatingComment);
        }
    }
}
