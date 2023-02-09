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

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPostsByInformationPublic
{
    public class GetInfoPostsByInformationPublicQueryHandler : IRequestHandler<GetInfoPostsByInformationPublicQuery, InfoPostsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetInfoPostsByInformationPublicQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostsListViewModel> Handle(GetInfoPostsByInformationPublicQuery request, CancellationToken cancellationToken)
        {
            var infoPosts = await _context.InfoPosts
                .Where(post => post.InfoPublicId == request.PublicId)
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
