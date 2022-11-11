using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : IRequest<CountryViewModel>
    {
        public string CountryName { get; set; }
    }
}
