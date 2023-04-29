using System;

namespace MyFaculty.Domain.Entities
{
    public class TaskSubmissionReview
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public Guid SubmissionReviewAttachmentsUid { get; set; }
        public string Attachments { get; set; }
        public int Rate { get; set; }
        public int SubmissionId { get; set; }
        public TaskSubmission TaskSubmission { get; set; }
        public int ReviewerId { get; set; }
        public AppUser Reviewer { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
