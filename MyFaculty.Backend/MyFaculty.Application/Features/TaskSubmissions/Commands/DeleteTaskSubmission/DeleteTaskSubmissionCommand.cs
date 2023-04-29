using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TaskSubmissions.Commands.DeleteTaskSubmission
{
    public class DeleteTaskSubmissionCommand : IRequest<TaskSubmissionViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
