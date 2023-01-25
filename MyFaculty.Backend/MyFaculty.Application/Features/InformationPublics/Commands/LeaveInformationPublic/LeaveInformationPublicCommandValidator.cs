using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic
{
    public class LeaveInformationPublicCommandValidator : AbstractValidator<LeaveInformationPublicCommand>
    {
        public LeaveInformationPublicCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.PublicId).NotEmpty();
        }
    }
}
