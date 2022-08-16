using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using MyFaculty.Application.ViewModels;
using System.Threading;
using MyFaculty.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using MyFaculty.Application.Dto;
using Microsoft.EntityFrameworkCore;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroups
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, GroupsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
