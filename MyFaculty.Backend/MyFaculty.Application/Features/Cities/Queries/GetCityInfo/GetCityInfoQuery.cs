using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Queries.GetCityInfo
{
    public class GetCityInfoQuery : IRequest<CityViewModel>
    {
        public int Id { get; set; }
    }
}
