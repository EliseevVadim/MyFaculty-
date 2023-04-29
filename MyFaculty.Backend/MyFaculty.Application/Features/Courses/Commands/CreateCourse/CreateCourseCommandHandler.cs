using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CourseViewModel> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            Course course = new Course()
            {
                CourseName = request.CourseName,
                CourseNumber = request.CourseNumber,
                FacultyId = request.FacultyId,
                Created = DateTime.Now
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CourseViewModel>(course);
        }
    }
}
