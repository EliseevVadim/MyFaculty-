using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateCommentCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentViewModel> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Post commentedPost = await _context.Posts.FirstOrDefaultAsync(post => post.Id == request.PostId);
            if (commentedPost == null)
                throw new EntityNotFoundException(nameof(Post), request.PostId);
            if (commentedPost is InfoPost)
            {
                InfoPost infoPost = await _context.InfoPosts
                    .Include(infoPost => infoPost.OwningInformationPublic)
                        .ThenInclude(infoPublic => infoPublic.BlockedUsers)
                    .FirstOrDefaultAsync(infoPost => infoPost.Id == request.PostId);
                if (infoPost.OwningInformationPublic != null && infoPost.OwningInformationPublic.IsBanned)
                    throw new EntityNotFoundException(nameof(InfoPost), request.PostId);
                if (!infoPost.CommentsAllowed)
                    throw new DestructiveActionException("Вы не можете прокомментировать эту запись, поскольку в ней запрещены комментарии");
                if (infoPost.OwningInformationPublic != null
                    && infoPost
                        .OwningInformationPublic
                        .BlockedUsers
                        .Any(user => user.Id == request.AuthorId))
                    throw new DestructiveActionException("Вы не можете прокомментировать эту запись, так как были заблокированы " +
                        "в сообществе, содержащем ее");
            }
            DateTime actionsDate = DateTime.Now;
            Comment comment = new Comment()
            {
                TextContent = request.TextContent,
                Attachments = request.Attachments,
                CommentAttachmentsUid = request.CommentAttachmentsUid,
                AuthorId = request.AuthorId,
                PostId = request.PostId,
                ParentCommentId = request.ParentCommentId,
                Created = actionsDate
            };
            await _context.Comments.AddAsync(comment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            if (request.ParentCommentId != null)
            {
                Comment parentComment = await _context.Comments.FindAsync(new object[] { request.ParentCommentId }, cancellationToken);
                if (parentComment == null)
                    throw new EntityNotFoundException(nameof(Comment), request.PostId);
                AppUser currentAuthor = await _context.Users.FindAsync(new object[] { request.AuthorId }, cancellationToken);
                if (currentAuthor == null)
                    throw new EntityNotFoundException(nameof(AppUser), request.PostId);
                if (parentComment.AuthorId != request.AuthorId)
                {
                    Notification notification = new Notification()
                    {
                        UserId = parentComment.AuthorId,
                        TextContent = $"{string.Join(" ", new string[] { currentAuthor.FirstName, currentAuthor.LastName })} ответил на Ваш комментарий...",
                        ReturnUrl = parentComment.Post is ClubTask ? $"/task{parentComment.PostId}" : $"/post{parentComment.PostId}",
                        MetaInfo = JsonConvert.SerializeObject(new
                        {
                            JumpToId = $"comment_{comment.Id}"
                        }),
                        Created = actionsDate
                    };
                    await _context.Notifications.AddAsync(notification);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return _mapper.Map<CommentViewModel>(comment);
        }
    }
}
