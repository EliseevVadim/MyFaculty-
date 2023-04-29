using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.JoinStudyClub
{
    public class JoinStudyClubCommand : IRequest
    {
        public int UserId { get; set; }
        public int StudyClubId { get; set; }
    }
}
