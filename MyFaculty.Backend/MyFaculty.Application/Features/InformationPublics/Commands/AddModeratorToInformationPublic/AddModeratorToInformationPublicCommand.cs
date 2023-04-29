using MediatR;

namespace MyFaculty.Application.Features.InformationPublics.Commands.AddModeratorToInformationPublic
{
    public class AddModeratorToInformationPublicCommand : IRequest<int>
    {
        public int IssuerId { get; set; }
        public int InformationPublicId { get; set; }
        public int ModeratorId { get; set; }
    }
}
