using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails
{
    public class GetPairInfoDetailsQuery : IRequest<PairInfoViewModel>
    {
        public int Id { get; set; }
    }
}
