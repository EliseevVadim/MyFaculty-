using MediatR;

namespace MyFaculty.Application.Features.InformationPublics.Commands.BlockUserAtInformationPublic
{
    public class BlockUserAtInformationPublicCommand : IRequest<int>
    {
        public int PublicId { get; set; }
        public int UserId { get; set; }
        public int IssuerId { get; set; }
    }
}
