using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionCommand : IRequest<RegionViewModel>
    {
        public string RegionName { get; set; }
        public int CountryId { get; set; }
    }
}
