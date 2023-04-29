using MediatR;
using MyFaculty.Application.ViewModels;
using System;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.CreateTaskSubmission
{
    public class CreateTaskSubmissionCommand : IRequest<TaskSubmissionViewModel>
    {
        public string Title { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public Guid SubmissionAttachmentsUid { get; set; }
        public int ClubTaskId { get; set; }
        public int AuthorId { get; set; }
    }
}
