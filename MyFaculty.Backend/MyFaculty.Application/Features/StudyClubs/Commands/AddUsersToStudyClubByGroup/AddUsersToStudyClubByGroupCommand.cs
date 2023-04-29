using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByGroup
{
    public class AddUsersToStudyClubByGroupCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int GroupId { get; set; }
        public int StudyClubId { get; set; }
    }
}
