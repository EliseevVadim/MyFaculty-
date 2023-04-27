using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.AddModeratorToInformationPublic
{
    public class AddModeratorToInformationPublicCommandValidator : AbstractValidator<AddModeratorToInformationPublicCommand>
    {
        public AddModeratorToInformationPublicCommandValidator()
        {
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.InformationPublicId).NotEmpty();
            RuleFor(command => command.ModeratorId).NotEmpty();
        }
    }
}
