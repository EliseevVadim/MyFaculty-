using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.RemoveUsersFromStudyClubByGroup
{
    public class RemoveUsersFromStudyClubByGroupCommand : IRequest
    {
        public int IssuerId { get; set; }
        public int GroupId { get; set; }
        public int StudyClubId { get; set; }
    }
}
