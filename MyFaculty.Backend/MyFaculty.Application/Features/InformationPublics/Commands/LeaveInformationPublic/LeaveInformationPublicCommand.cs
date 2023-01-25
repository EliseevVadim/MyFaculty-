using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic
{
    public class LeaveInformationPublicCommand : IRequest
    {
        public int UserId { get; set; }
        public int PublicId { get; set; }
    }
}
