using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
