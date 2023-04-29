using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionsForCountry
{
    public class GetRegionsForCountryQueryHandler : IRequestHandler<GetRegionsForCountryQuery, RegionsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetRegionsForCountryQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegionsListViewModel> Handle(GetRegionsForCountryQuery request, CancellationToken cancellationToken)
        {
            var regions = await _context.Regions
                .Where(region => region.CountryId == request.CountryId)
                .ProjectTo<RegionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new RegionsListViewModel()
            {
                Regions = regions
            };
        }
    }
}
