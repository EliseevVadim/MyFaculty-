using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairsForGroup
{
    public class GetPairsForGroupQuery : IRequest<PairsListViewModel>
    {
        public int GroupId { get; set; }
    }
}
