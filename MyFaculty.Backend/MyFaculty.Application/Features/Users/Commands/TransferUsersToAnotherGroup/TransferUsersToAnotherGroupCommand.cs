using MediatR;

namespace MyFaculty.Application.Features.Users.Commands.TransferUsersToAnotherGroup
{
    public class TransferUsersToAnotherGroupCommand : IRequest
    {
        public int SourceGroupId { get; set; }
        public int DestinationGroupId { get; set; }
    }
}
