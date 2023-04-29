using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.UpdateSecondaryObject
{
    public class UpdateSecondaryObjectCommand : IRequest<SecondaryObjectViewModel>
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string PositionInfo { get; set; }
        public int SecondaryObjectTypeId { get; set; }
        public int FloorId { get; set; }
    }
}
