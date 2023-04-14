using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.TransferUsersToAnotherGroup
{
    public class TransferUsersToAnotherGroupCommand : IRequest
    {
        public int SourceGroupId { get; set; }
        public int DestinationGroupId { get; set; }
    }
}
