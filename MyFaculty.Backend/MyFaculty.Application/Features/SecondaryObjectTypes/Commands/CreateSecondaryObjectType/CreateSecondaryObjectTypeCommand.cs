using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType
{
    public class CreateSecondaryObjectTypeCommand : IRequest<SecondaryObjectTypeViewModel>
    {
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }
    }
}
