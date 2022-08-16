using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Course>
    {
        private IMFDbContext _context;

        public CreateCourseCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            Course course = new Course()
            {
                CourseName = request.CourseName,
                CourseNumber = request.CourseNumber,
                Created = DateTime.Now
            };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync(cancellationToken);
            return course;
        }
    }
}
