using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByCourse
{
    public class RemoveUsersFromStudyClubByCourseCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int CourseId { get; set; }
        public int StudyClubId { get; set; }
    }
}
