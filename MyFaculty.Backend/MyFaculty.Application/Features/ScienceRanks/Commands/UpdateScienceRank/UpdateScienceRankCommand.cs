using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank
{
    public class UpdateScienceRankCommand : IRequest<ScienceRankViewModel>
    {
        public int Id { get; set; }
        public string RankName { get; set; }
    }
}
