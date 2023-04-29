using FluentValidation;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.CreateTaskSubmissionReview
{
    public class CreateTaskSubmissionReviewCommandValidator : AbstractValidator<CreateTaskSubmissionReviewCommand>
    {
        public CreateTaskSubmissionReviewCommandValidator()
        {
            RuleFor(command => command.SubmissionReviewAttachmentsUid).NotEmpty();
            RuleFor(command => command.SubmissionId).NotEmpty();
            RuleFor(command => command.ReviewerId).NotEmpty();
            RuleFor(command => command.Rate).GreaterThanOrEqualTo(0);
            RuleFor(command => command.NewStatus).NotEmpty();
        }
    }
}
