using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Queries.GetCities
{
    public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, CitiesListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitiesListViewModel> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities
                .ProjectTo<CityLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new CitiesListViewModel()
            {
                Cities = cities
            };
        }
    }
}
