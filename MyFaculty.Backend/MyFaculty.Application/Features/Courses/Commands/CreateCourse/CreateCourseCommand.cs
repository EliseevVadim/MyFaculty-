using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<Course>
    {
        public string CourseName { get; set; }
        public int CourseNumber { get; set; }
    }
}
