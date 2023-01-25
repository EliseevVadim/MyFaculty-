using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.CreateInformationPublic
{
    public class CreateInformationPublicCommandValidator : AbstractValidator<CreateInformationPublicCommand>
    {
        public CreateInformationPublicCommandValidator()
        {
            RuleFor(command => command.PublicName).NotEmpty().MaximumLength(255);
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
