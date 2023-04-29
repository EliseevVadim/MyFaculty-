using MediatR;

namespace MyFaculty.Application.Features.Pairs.Commands.DeletePair
{
    public class DeletePairCommand : IRequest
    {
        public int Id { get; set; }
    }
}
