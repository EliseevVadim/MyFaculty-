using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.DeleteTaskSubmissionReview
{
    public class DeleteTaskSubmissionReviewCommand : IRequest<TaskSubmissionReviewViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
