using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<CountriesListViewModel>
    {

    }
}
