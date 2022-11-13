using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Queries.GetCitiesForRegion
{
    public class GetCitiesForRegionQueryHandler : IRequestHandler<GetCitiesForRegionQuery, CitiesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetCitiesForRegionQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CitiesListViewModel> Handle(GetCitiesForRegionQuery request, CancellationToken cancellationToken)
        {
            var cities = await _context.Cities
                .Where(city => city.RegionId == request.RegionId)
                .ProjectTo<CityLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new CitiesListViewModel()
            {
                Cities = cities
            };
        }
    }
}
