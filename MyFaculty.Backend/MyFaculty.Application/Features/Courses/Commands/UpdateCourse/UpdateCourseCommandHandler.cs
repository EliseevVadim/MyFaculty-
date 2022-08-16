using MyFaculty.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Domain.Entities;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;

namespace MyFaculty.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Course>
    {
        private IMFDbContext _context;

        public UpdateCourseCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Course> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            Course course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == request.Id, cancellationToken);
            if (course == null)
                throw new EntityNotFoundException(nameof(Course), request.Id);
            course.CourseName = request.CourseName;
            course.CourseNumber = request.CourseNumber;
            course.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return course;
        }
    }
}
