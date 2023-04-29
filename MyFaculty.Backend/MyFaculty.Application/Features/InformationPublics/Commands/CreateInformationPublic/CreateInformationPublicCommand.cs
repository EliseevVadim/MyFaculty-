using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InformationPublics.Commands.CreateInformationPublic
{
    public class CreateInformationPublicCommand : IRequest<InformationPublicViewModel>
    {
        public string PublicName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
    }
}
