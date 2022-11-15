using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionInfo
{
    public class GetRegionInfoQuery : IRequest<RegionViewModel>
    {
        public int Id { get; set; }
    }
}
