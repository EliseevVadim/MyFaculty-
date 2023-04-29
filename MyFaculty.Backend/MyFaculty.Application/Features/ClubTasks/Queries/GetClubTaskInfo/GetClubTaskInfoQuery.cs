using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTaskInfo
{
    public class GetClubTaskInfoQuery : IRequest<ClubTaskViewModel>
    {
        public int Id { get; set; }
    }
}
