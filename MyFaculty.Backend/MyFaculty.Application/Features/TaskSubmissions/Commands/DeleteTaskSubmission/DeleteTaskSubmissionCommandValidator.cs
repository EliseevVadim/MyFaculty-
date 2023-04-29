using FluentValidation;

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
