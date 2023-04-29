using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubInfo
{
    public class GetStudyClubInfoQuery : IRequest<StudyClubViewModel>
    {
        public int Id { get; set; }
    }
}
