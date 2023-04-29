using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRankInfo
{
    public class GetScienceRankInfoQuery : IRequest<ScienceRankViewModel>
    {
        public int Id { get; set; }
    }
}
