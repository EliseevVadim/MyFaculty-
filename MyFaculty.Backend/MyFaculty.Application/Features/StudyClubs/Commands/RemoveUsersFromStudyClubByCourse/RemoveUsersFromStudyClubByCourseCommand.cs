using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByCourse
{
    public class RemoveUsersFromStudyClubByCourseCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int CourseId { get; set; }
        public int StudyClubId { get; set; }
    }
}
