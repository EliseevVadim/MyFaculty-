using MediatR;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UnbanInformationPublic
{
    public class UnbanInformationPublicCommand : IRequest
    {
        public int UnbannedPublicId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
    }
}
