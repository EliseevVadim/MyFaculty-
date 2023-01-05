using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Groups.Queries.GetGroupsForCourse
{
    public class GetGroupsForCourseQueryHandler : IRequestHandler<GetGroupsForCourseQuery, GroupsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetGroupsForCourseQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GroupsListViewModel> Handle(GetGroupsForCourseQuery request, CancellationToken cancellationToken)
        {
            var groups = await _context.Groups
                .Where(group => group.CourseId == request.CourseId)
                .ProjectTo<GroupLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new GroupsListViewModel()
            {
                Groups = groups
            };
        }
    }
}
