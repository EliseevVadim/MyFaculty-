using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo
{
    public class UpdatePairInfoCommand : IRequest<PairInfo>
    {
        public int Id { get; set; }
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
