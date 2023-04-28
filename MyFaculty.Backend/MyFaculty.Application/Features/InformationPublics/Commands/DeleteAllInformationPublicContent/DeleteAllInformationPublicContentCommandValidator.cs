using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteAllInformationPublicContent
{
    public class DeleteAllInformationPublicContentCommandValidator : AbstractValidator<DeleteAllInformationPublicContentCommand>
    {
        public DeleteAllInformationPublicContentCommandValidator()
        {
            RuleFor(command => command.PublicId).NotEmpty().GreaterThan(1);
        }
    }
}
