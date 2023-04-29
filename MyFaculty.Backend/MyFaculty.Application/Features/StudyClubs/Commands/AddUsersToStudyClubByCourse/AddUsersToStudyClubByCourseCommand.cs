using MediatR;

namespace MyFaculty.Application.Features.StudyClubs.Commands.AddUsersToStudyClubByCourse
{
    public class AddUsersToStudyClubByCourseCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int CourseId { get; set; }
        public int StudyClubId { get; set; }
    }
}
