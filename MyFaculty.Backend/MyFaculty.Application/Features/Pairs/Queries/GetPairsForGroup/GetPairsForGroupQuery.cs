using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairsForGroup
{
    public class GetPairsForGroupQuery : IRequest<PairsListViewModel>
    {
        public int GroupId { get; set; }
    }
}
