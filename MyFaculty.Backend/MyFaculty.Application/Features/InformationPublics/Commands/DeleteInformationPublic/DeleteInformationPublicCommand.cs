using MediatR;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteInformationPublic
{
    public class DeleteInformationPublicCommand : IRequest
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
