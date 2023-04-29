using MediatR;

namespace MyFaculty.Application.Features.InformationPublics.Commands.LeaveInformationPublic
{
    public class LeaveInformationPublicCommand : IRequest
    {
        public int UserId { get; set; }
        public int PublicId { get; set; }
    }
}
