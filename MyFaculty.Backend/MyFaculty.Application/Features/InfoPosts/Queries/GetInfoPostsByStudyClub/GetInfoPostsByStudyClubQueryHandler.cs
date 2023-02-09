using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByStudyClub
{
    public class GetInfoPostsByStudyClubQueryHandler : IRequestHandler<GetInfoPostsByStudyClubQuery, InfoPostsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetInfoPostsByStudyClubQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostsListViewModel> Handle(GetInfoPostsByStudyClubQuery request, CancellationToken cancellationToken)
        {
            var infoPosts = await _context.InfoPosts
                .Where(post => post.StudyClubId == request.StudyClubId)
                .OrderByDescending(post => post.Created)
                .ProjectTo<InfoPostViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new InfoPostsListViewModel()
            {
                InfoPosts = infoPosts
            };
        }
    }
}
