using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails
{
    public class GetPairInfoDetailsQuery : IRequest<PairInfoViewModel>
    {
        public int Id { get; set; }
    }
}
