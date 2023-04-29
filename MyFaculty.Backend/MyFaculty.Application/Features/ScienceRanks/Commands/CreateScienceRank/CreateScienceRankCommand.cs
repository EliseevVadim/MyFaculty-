using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank
{
    public class CreateScienceRankCommand : IRequest<ScienceRankViewModel>
    {
        public string RankName { get; set; }
    }
}
