using MediatR;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.DeleteScienceRank
{
    public class DeleteScienceRankCommand : IRequest
    {
        public int Id { get; set; }
    }
}
