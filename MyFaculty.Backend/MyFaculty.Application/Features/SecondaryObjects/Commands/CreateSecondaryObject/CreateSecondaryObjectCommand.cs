using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.CreateSecondaryObject
{
    public class CreateSecondaryObjectCommand : IRequest<SecondaryObjectViewModel>
    {
        public string ObjectName { get; set; }
        public string PositionInfo { get; set; }
        public int SecondaryObjectTypeId { get; set; }
        public int FloorId { get; set; }
    }
}
