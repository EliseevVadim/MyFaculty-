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
            RuleFor(command => command.TimezoneOffset).NotEmpty();
            RuleFor(command => command.DeadLine.AddMinutes(-command.TimezoneOffset))
                .NotEmpty().GreaterThan(DateTime.Now);
        }
    }
}