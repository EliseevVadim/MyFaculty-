using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.CreateTaskSubmission
{
    public class CreateTaskSubmissionCommandValidator : AbstractValidator<CreateTaskSubmissionCommand>
    {
        public CreateTaskSubmissionCommandValidator()
        {
            RuleFor(command => command.Title).NotEmpty();
            RuleFor(command => command.SubmissionAttachmentsUid).NotEmpty();
            RuleFor(command => command.ClubTaskId).NotEmpty();
            RuleFor(command => command.AuthorId).NotEmpty();
        }
    }
}
