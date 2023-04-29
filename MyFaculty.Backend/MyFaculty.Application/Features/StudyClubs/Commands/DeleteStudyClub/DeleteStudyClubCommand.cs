using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.DeleteStudyClub
{
    public class DeleteStudyClubCommand : IRequest
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
