using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsForSpecificUser
{
    public class GetStudyClubsForSpecificUserQuery : IRequest<StudyClubsListViewModel>
    {
        public int UserId { get; set; }
    }
}
