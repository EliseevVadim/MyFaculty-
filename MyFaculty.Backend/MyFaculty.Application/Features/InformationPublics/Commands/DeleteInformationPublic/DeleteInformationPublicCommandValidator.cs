using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteInformationPublic
{
    public class DeleteInformationPublicCommandValidator : AbstractValidator<DeleteInformationPublicCommand>
    {
        public DeleteInformationPublicCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
