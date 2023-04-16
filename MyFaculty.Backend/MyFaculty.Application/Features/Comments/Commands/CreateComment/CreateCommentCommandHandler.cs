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
                if (infoPost.OwningInformationPublic != null
                    && infoPost
                        .OwningInformationPublic
                        .BlockedUsers
                        .Select(user => user.Id)
                        .Contains(request.AuthorId))
                    throw new DestructiveActionException("Вы не можете прокомментировать эту запись, так как были заблокированы " +
                        "в сообществе, содержащем ее");
            }
            DateTime actionsDate = DateTime.Now;
            if (request.ParentCommentId != null)
            {
                Comment parentComment = await _context.Comments
                    .Include(comment => comment.Post)
                    .Include(comment => comment.Author)
                    .FirstOrDefaultAsync(comment => comment.Id == request.ParentCommentId, cancellationToken);
                if (parentComment == null)
                    throw new EntityNotFoundException(nameof(Comment), request.PostId);
                Notification notification = new Notification()
                {
                    UserId = parentComment.AuthorId,
                    TextContent = $"{string.Concat(new string[] {parentComment.Author.FirstName, parentComment.Author.LastName})} ответил на Ваш комментарий...",
                    ReturnUrl = parentComment.Post == null ? $"/task{parentComment.PostId}" : $"/post{parentComment.PostId}",
                    MetaInfo = JsonConvert.SerializeObject(new
                    {
                        JumpToId = $"comment_{parentComment.Id}" 
                    }),
                    Created = actionsDate
                };
                await _context.Notifications.AddAsync(notification);
            }
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
            return _mapper.Map<CommentViewModel>(comment);
        }
    }
}
