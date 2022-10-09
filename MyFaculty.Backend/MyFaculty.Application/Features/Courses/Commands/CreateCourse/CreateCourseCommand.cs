using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<CourseViewModel>
    {
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        public int FacultyId { get; set; }
    }
}
