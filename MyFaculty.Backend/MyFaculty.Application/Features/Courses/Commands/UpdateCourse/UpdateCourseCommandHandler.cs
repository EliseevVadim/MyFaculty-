using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

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
            course.FacultyId = request.FacultyId;
            course.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CourseViewModel>(course);
        }
    }
}
