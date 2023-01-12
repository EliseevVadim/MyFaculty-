using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.JoinStudyClub
{
    public class JoinStudyClubCommand : IRequest
    {
        public int UserId { get; set; }
        public int StudyClubId { get; set; }
    }
}
