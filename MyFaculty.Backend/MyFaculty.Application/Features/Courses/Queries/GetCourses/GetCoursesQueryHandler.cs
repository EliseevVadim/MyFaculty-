using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourses
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, CoursesListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetCoursesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoursesListViewModel> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new CoursesListViewModel()
            {
                Courses = courses
            };
        }
    }
}
