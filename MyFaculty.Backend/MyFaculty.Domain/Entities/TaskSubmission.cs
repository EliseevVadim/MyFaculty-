using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class TaskSubmission
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public Guid SubmissionAttachmentsUid { get; set; }
        public string Attachments { get; set; }
        public TaskSubmissionStatus Status { get; set; }
        public int ClubTaskId { get; set; }
        public ClubTask ClubTask { get; set; }
        public int AuthorId { get; set; }
        public AppUser Author { get; set; }
        public int? ReviewId { get; set; }
        public TaskSubmissionReview Review { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
