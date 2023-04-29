using FluentValidation;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.DeleteTaskSubmissionReview
{
    public class DeleteTaskSubmissionReviewCommandValidator : AbstractValidator<DeleteTaskSubmissionReviewCommand>
    {
        public DeleteTaskSubmissionReviewCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
        }
    }
}
