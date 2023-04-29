using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTasksForStudyClub
{
    public class GetClubTasksForStudyClubQuery : IRequest<ClubTasksListViewModel>
    {
        public int StudyClubId { get; set; }
    }
}
