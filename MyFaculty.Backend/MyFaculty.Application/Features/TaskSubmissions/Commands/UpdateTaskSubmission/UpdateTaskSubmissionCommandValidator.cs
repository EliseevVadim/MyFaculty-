using FluentValidation;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.UpdateTaskSubmission
{
    public class UpdateTaskSubmissionCommandValidator : AbstractValidator<UpdateTaskSubmissionCommand>
    {
        public UpdateTaskSubmissionCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.Title).NotEmpty();
        }
    }
}
