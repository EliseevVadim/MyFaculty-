using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Queries.GetCityInfo
{
    public class GetCityInfoQueryHandler : IRequestHandler<GetCityInfoQuery, CityViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetCityInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityViewModel> Handle(GetCityInfoQuery request, CancellationToken cancellationToken)
        {
            City city = await _context.Cities
                .Include(city => city.Region)
                    .ThenInclude(region => region.Country)
                .FirstOrDefaultAsync(city => city.Id == request.Id);
            if (city == null)
                throw new EntityNotFoundException(nameof(City), request.Id);
            return _mapper.Map<CityViewModel>(city);
        }
    }
}
