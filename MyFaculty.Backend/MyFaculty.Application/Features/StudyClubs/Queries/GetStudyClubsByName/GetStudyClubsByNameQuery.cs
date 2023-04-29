using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsByName
{
    public class GetStudyClubsByNameQuery : IRequest<StudyClubsListViewModel>
    {
        public string SearchRequest { get; set; }
    }
}
