using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountries
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, CountriesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetCountriesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountriesListViewModel> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var countries = await _context.Countries
                .ProjectTo<CountryLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new CountriesListViewModel()
            {
                Countries = countries
            };
        }
    }
}
