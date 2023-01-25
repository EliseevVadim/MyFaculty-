using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnblockUserAtInformationPublic
{
    public class UnblockUserAtInformationPublicCommand : IRequest
    {
        public int PublicId { get; set; }
        public int UserId { get; set; }
        public int IssuerId { get; set; }
    }
}
