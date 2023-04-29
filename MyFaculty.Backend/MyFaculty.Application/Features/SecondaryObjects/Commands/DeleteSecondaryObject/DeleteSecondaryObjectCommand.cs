using MediatR;

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.DeleteSecondaryObject
{
    public class DeleteSecondaryObjectCommand : IRequest
    {
        public int Id { get; set; }
    }
}
