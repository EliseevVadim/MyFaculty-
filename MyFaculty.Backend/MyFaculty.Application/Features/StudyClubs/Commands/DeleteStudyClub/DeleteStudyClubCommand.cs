using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.DeleteStudyClub
{
    public class DeleteStudyClubCommand : IRequest
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
