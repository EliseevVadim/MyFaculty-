using MediatR;

namespace MyFaculty.Application.Features.InformationPublics.Commands.BanInformationPublic
{
    public class BanInformationPublicCommand : IRequest
    {
        public int BannedPublicId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
    }
}
