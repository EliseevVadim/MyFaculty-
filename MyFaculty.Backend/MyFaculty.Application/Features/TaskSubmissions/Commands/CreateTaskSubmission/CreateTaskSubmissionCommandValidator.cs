using FluentValidation;

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
