using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType
{
    public class UpdateSecondaryObjectTypeCommand : IRequest<SecondaryObjectTypeViewModel>
    {
        public int Id { get; set; }
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }
    }
}
