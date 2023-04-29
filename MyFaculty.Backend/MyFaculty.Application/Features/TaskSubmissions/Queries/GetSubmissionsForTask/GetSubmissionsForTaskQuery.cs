using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetSubmissionsForTask
{
    public class GetSubmissionsForTaskQuery : IRequest<TaskSubmissionsListViewModel>
    {
        public int IssuerId { get; set; }
        public int ClubTaskId { get; set; }
    }
}
