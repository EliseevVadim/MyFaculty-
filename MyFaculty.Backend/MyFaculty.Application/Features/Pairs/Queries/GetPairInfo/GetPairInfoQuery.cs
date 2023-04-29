using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairInfo
{
    public class GetPairInfoQuery : IRequest<PairViewModel>
    {
        public int Id { get; set; }
    }
}
