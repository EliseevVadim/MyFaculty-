using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.JoinInformationPublic
{
    public class JoinInformationPublicCommandValidator : AbstractValidator<JoinInformationPublicCommand>
    {
        public JoinInformationPublicCommandValidator()
        {
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.PublicId).NotEmpty();
        }
    }
}
