using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TaskSubmissions.Queries.GetMineSubmissionsForTask
{
    public class GetMineSubmissionsForTaskQuery : IRequest<TaskSubmissionsListViewModel>
    {
        public int IssuerId { get; set; }
        public int ClubTaskId { get; set; }
    }
}
