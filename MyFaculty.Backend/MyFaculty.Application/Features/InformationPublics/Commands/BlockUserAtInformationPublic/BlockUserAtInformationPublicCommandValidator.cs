using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.BlockUserAtInformationPublic
{
    public class BlockUserAtInformationPublicCommandValidator : AbstractValidator<BlockUserAtInformationPublicCommand>
    {
        public BlockUserAtInformationPublicCommandValidator()
        {
            RuleFor(command => command.PublicId).NotEmpty();
            RuleFor(command => command.UserId).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
