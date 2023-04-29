using MediatR;

namespace MyFaculty.Application.Features.InformationPublics.Commands.JoinInformationPublic
{
    public class JoinInformationPublicCommand : IRequest
    {
        public int UserId { get; set; }
        public int PublicId { get; set; }
    }
}
