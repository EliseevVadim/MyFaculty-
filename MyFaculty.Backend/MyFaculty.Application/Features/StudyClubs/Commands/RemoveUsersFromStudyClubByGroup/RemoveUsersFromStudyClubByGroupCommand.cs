using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByGroup
{
    public class RemoveUsersFromStudyClubByGroupCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int GroupId { get; set; }
        public int StudyClubId { get; set; }
    }
}
