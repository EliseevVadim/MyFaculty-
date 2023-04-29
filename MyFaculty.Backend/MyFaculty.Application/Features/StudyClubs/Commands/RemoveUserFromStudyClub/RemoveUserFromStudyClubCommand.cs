using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUserFromStudyClub
{
    public class RemoveUserFromStudyClubCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int RemovingUserId { get; set; }
        public int StudyClubId { get; set; }
    }
}
