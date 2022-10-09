using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace MyFaculty.Application.Features.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.CourseName).NotEmpty().MaximumLength(250);
            RuleFor(command => command.CourseNumber).NotEmpty();
            RuleFor(command => command.FacultyId).NotEmpty();
        }
    }
}
