using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Comments.Queries.GetCommentsForPost
{
    public class GetCommentsForPostQueryHandler : IRequestHandler<GetCommentsForPostQuery, CommentsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetCommentsForPostQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommentsListViewModel> Handle(GetCommentsForPostQuery request, CancellationToken cancellationToken)
        {
            Post owningPost = await _context.Posts.FirstOrDefaultAsync(post => post.Id == request.PostId);
            if (owningPost == null)
                throw new EntityNotFoundException(nameof(Post), request.PostId);
            if (owningPost is InfoPost)
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
                        .Contains(request.IssuerId))
                    throw new UnauthorizedActionException("Вы не можете просмотривать записи этого сообщества, так как были заблокированы в нем");
            }
            var comments = await _context.Comments
                .Where(comment => comment.PostId == request.PostId)
                .OrderByDescending(comment => comment.Created)
                .ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new CommentsListViewModel()
            {
                Comments = comments
            };
        }
    }
}
