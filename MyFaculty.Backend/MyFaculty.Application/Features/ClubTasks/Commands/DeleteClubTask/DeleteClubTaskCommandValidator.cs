using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Commands.DeleteClubTask
{
    public class DeleteClubTaskCommandValidator : AbstractValidator<DeleteClubTaskCommand>
    {
        public DeleteClubTaskCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
