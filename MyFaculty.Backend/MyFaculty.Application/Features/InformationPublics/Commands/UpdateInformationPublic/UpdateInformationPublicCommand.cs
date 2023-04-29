using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.InformationPublics.Commands.UpdateInformationPublic
{
    public class UpdateInformationPublicCommand : IRequest<InformationPublicViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public string PublicName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
    }
}
