using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.UpdateTaskSubmission
{
    public class UpdateTaskSubmissionCommand : IRequest<TaskSubmissionViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
    }
}
