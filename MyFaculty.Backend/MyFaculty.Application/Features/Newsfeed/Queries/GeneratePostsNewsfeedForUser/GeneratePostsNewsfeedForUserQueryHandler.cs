using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Newsfeed.Queries.GeneratePostsNewsfeedForUser
{
    public class GeneratePostsNewsfeedForUserQueryHandler : IRequestHandler<GeneratePostsNewsfeedForUserQuery, InfoPostsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        private const int POSTS_COUNT_LIMIT = 100;

        public GeneratePostsNewsfeedForUserQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostsListViewModel> Handle(GeneratePostsNewsfeedForUserQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _context.Users
                .Include(user => user.InformationPublics)
                    .ThenInclude(infoPublic => infoPublic.InfoPosts)
                        .ThenInclude(infoPost => infoPost.OwningInformationPublic)
                .Include(user => user.InformationPublics)
                    .ThenInclude(infoPublic => infoPublic.InfoPosts)
                        .ThenInclude(infoPost => infoPost.Author)
                .Include(user => user.InformationPublics)
                    .ThenInclude(infoPublic => infoPublic.InfoPosts)
                        .ThenInclude(infoPost => infoPost.Comments)
                .Include(user => user.InformationPublics)
                    .ThenInclude(infoPublic => infoPublic.InfoPosts)
                        .ThenInclude(infoPost => infoPost.LikedUsers)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.Moderators)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.InfoPosts)
                        .ThenInclude(infoPost => infoPost.OwningStudyClub)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.InfoPosts)
                        .ThenInclude(infoPost => infoPost.Author)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.InfoPosts)
                        .ThenInclude(infoPost => infoPost.Comments)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.InfoPosts)
                        .ThenInclude(infoPost => infoPost.LikedUsers)
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (user == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            var infoPosts = user.InformationPublics
                .SelectMany(infoPublic => infoPublic.InfoPosts)
                .Where(infoPost => !infoPost.OwningInformationPublic.IsBanned)
                .Concat(user.StudyClubs.SelectMany(club => club.InfoPosts))
                .OrderByDescending(infoPost => infoPost.Created)
                .Take(POSTS_COUNT_LIMIT)
                .Select(infoPost => _mapper.Map<InfoPostViewModel>(infoPost))
                .ToList();
            return new InfoPostsListViewModel()
            {
                InfoPosts = infoPosts
            };
        }
    }
}
