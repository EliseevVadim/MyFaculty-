using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddModeratorToStudyClub
{
    public class AddModeratorToStudyClubCommand : IRequest<int>
    {
        public int IssuerId { get; set; }
        public int StudyClubId { get; set; }
        public int ModeratorId { get; set; }
    }
}
