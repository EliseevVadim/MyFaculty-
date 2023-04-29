using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.LeaveStudyClub
{
    public class LeaveStudyClubCommand : IRequest
    {
        public int UserId { get; set; }
        public int StudyClubId { get; set; }
    }
}
