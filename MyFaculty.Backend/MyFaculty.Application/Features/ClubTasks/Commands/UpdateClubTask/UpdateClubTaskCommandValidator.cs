using FluentValidation;
using System;

namespace MyFaculty.Application.Features.ClubTasks.Commands.UpdateClubTask
{
    public class UpdateClubTaskCommandValidator : AbstractValidator<UpdateClubTaskCommand>
    {
        public UpdateClubTaskCommandValidator()
        {
            RuleFor(command => command.TaskId).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.DeadLine).NotEmpty().GreaterThan(DateTime.Now);
        }
    }
}