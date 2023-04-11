using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TaskSubmissionReviews.Commands.CreateTaskSubmissionReview
{
    public class CreateTaskSubmissionReviewCommand : IRequest<TaskSubmissionReviewViewModel>
    {
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public Guid SubmissionReviewAttachmentsUid { get; set; }
        public int Rate { get; set; }
        public TaskSubmissionStatus NewStatus { get; set; }
        public int SubmissionId { get; set; }
        public int ReviewerId { get; set; }
    }
}
