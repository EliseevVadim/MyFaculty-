using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Enums;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.UpdateTaskSubmissionReview
{
    public class UpdateTaskSubmissionReviewCommand : IRequest<TaskSubmissionReviewViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public int Rate { get; set; }
        public TaskSubmissionStatus NewStatus { get; set; }
    }
}
