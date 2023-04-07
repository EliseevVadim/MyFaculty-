using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.CreateTaskSubmissionReview
{
    public class CreateTaskSubmissionReviewCommandValidator : AbstractValidator<CreateTaskSubmissionReviewCommand>
    {
        public CreateTaskSubmissionReviewCommandValidator()
        {
            RuleFor(command => command.SubmissionReviewAttachmentsUid).NotEmpty();
            RuleFor(command => command.SubmissionId).NotEmpty();
            RuleFor(command => command.ReviewerId).NotEmpty();
            RuleFor(command => command.Rate).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
