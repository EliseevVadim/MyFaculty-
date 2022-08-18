using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank
{
    public class CreateScienceRankCommand : IRequest<ScienceRankViewModel>
    {
        public string RankName { get; set; }
    }
}
