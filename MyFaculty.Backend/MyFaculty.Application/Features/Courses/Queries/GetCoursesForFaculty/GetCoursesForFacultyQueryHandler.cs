using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Courses.Queries.GetCoursesForFaculty
{
    public class GetCoursesForFacultyQueryHandler : IRequestHandler<GetCoursesForFacultyQuery, CoursesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetCoursesForFacultyQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoursesListViewModel> Handle(GetCoursesForFacultyQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
                .Where(course => course.FacultyId == request.FacultyId)
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new CoursesListViewModel()
            {
                Courses = courses
            };
        }
    }
}
