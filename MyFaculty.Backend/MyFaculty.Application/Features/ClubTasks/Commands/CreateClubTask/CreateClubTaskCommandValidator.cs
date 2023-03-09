using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Commands.CreateClubTask
{
    public class CreateClubTaskCommandValidator : AbstractValidator<CreateClubTaskCommand>
    {
        public CreateClubTaskCommandValidator()
        {
            RuleFor(command => command.StudyClubId).NotEmpty();
            RuleFor(command => command.PostAttachmentsUid).NotEmpty();
            RuleFor(command => command.AuthorId).NotEmpty();
            RuleFor(command => command.TimezoneOffset).NotEmpty();
            RuleFor(command => command.Cost).NotEmpty().GreaterThan(0);
            RuleFor(command => command.DeadLine.AddMinutes(-command.TimezoneOffset))
                .NotEmpty().GreaterThan(DateTime.Now);
        }
    }
}
