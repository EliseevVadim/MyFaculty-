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
using MyFaculty.Application.ViewModels;
using AutoMapper;

namespace MyFaculty.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateCourseCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CourseViewModel> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            Course course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == request.Id, cancellationToken);
            if (course == null)
                throw new EntityNotFoundException(nameof(Course), request.Id);
            course.CourseName = request.CourseName;
            course.CourseNumber = request.CourseNumber;
            course.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CourseViewModel>(course);
        }
    }
}
