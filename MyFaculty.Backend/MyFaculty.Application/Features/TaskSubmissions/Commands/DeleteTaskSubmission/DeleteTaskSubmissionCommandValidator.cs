using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.DeleteTaskSubmission
{
    public class DeleteTaskSubmissionCommandValidator : AbstractValidator<DeleteTaskSubmissionCommand>
    {
        public DeleteTaskSubmissionCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
