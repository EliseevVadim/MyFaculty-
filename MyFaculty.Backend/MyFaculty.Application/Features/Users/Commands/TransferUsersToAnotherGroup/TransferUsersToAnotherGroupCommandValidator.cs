using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.TransferUsersToAnotherGroup
{
    public class TransferUsersToAnotherGroupCommandValidator : AbstractValidator<TransferUsersToAnotherGroupCommand>
    {
        public TransferUsersToAnotherGroupCommandValidator()
        {
            RuleFor(command => command.SourceGroupId).NotEmpty();
            RuleFor(command => command.DestinationGroupId).NotEmpty();
        }
    }
}
