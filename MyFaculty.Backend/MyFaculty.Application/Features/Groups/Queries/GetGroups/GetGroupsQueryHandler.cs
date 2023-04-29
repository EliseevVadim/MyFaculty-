using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroups
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, GroupsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetGroupsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupsListViewModel> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups
                .ProjectTo<GroupLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new GroupsListViewModel()
            {
                Groups = groups
            };
        }
    }
}
