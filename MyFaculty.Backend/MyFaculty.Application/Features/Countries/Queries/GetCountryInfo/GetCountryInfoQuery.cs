using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountryInfo
{
    public class GetCountryInfoQuery : IRequest<CountryViewModel>
    {
        public int Id { get; set; }
    }
}
