using MediatR;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.DeleteSecondaryObjectType
{
    public class DeleteSecondaryObjectTypeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
