using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Queries.GetCountryInfo
{
    public class GetCountryInfoQueryHandler : IRequestHandler<GetCountryInfoQuery, CountryViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetCountryInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryViewModel> Handle(GetCountryInfoQuery request, CancellationToken cancellationToken)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(country => country.Id == request.Id);
            if (country == null)
                throw new EntityNotFoundException(nameof(Country), request.Id);
            return _mapper.Map<CountryViewModel>(country);
        }
    }
}
