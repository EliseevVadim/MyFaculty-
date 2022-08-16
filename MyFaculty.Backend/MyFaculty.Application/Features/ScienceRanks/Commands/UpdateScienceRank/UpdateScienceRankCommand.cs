using MediatR;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank
{
    public class UpdateScienceRankCommand : IRequest<ScienceRank>
    {
        public int Id { get; set; }
        public string RankName { get; set; }
    }
}
