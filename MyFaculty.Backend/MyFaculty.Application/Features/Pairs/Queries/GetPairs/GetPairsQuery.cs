using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairs
{
    public class GetPairsQuery : IRequest<PairsListViewModel>
    {

    }
}
