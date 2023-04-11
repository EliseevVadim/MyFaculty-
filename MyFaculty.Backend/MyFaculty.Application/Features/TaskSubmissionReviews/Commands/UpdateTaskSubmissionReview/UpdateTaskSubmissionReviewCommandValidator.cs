using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.UpdateTaskSubmissionReview
{
    public class UpdateTaskSubmissionReviewCommandValidator : AbstractValidator<UpdateTaskSubmissionReviewCommand>
    {
        public UpdateTaskSubmissionReviewCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();
            RuleFor(command => command.IssuerId).NotEmpty();
            RuleFor(command => command.Rate).GreaterThanOrEqualTo(0);
            RuleFor(command => command.NewStatus).NotEmpty();
        }
    }
}
