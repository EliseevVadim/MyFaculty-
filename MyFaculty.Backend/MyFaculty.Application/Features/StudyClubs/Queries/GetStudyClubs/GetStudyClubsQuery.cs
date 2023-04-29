using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubs
{
    public class GetStudyClubsQuery : IRequest<StudyClubsListViewModel>
    {

    }
}
