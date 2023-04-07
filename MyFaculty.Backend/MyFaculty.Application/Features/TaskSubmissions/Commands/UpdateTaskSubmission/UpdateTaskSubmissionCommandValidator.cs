using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.UpdateTaskSubmission
{
    public class UpdateTaskSubmissionCommandValidator : AbstractValidator<UpdateTaskSubmissionCommand>
    {
        public UpdateTaskSubmissionCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.Title).NotEmpty();
            RuleFor(command => command.Status).NotEmpty();
        }
    }
}
