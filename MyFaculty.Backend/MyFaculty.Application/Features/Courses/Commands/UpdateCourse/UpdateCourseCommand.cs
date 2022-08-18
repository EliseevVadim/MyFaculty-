using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<CourseViewModel>
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
    }
}
