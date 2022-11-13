using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionsForCountry
{
    public class GetRegionsForCountryQuery : IRequest<RegionsListViewModel>
    {
        public int CountryId { get; set; }
    }
}
