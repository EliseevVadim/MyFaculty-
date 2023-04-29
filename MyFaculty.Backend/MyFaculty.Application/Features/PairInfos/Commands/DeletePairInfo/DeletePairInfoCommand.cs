using MediatR;

namespace MyFaculty.Application.Features.PairInfos.Commands.DeletePairInfo
{
    public class DeletePairInfoCommand : IRequest
    {
        public int Id { get; set; }
    }
}
