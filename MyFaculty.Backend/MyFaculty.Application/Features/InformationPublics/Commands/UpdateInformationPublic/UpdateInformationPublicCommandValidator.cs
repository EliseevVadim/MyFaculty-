using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UpdateInformationPublic
{
    public class UpdateInformationPublicCommandValidator : AbstractValidator<UpdateInformationPublicCommand>
    {
        public UpdateInformationPublicCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.PublicName).NotEmpty().MaximumLength(255);
            RuleFor(command => command.OwnerId).NotEmpty();
        }
    }
}
