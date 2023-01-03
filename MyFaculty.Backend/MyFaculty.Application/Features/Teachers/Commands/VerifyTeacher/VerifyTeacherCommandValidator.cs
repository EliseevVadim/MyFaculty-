using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Commands.VerifyTeacher
{
    public class VerifyTeacherCommandValidator : AbstractValidator<VerifyTeacherCommand>
    {
        public VerifyTeacherCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.VerificationToken).NotEmpty();
        }
    }
}
