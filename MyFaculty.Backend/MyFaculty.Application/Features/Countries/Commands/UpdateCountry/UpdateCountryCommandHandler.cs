using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryViewModel> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = await _context.Countries.FirstOrDefaultAsync(country => country.Id == request.Id);
            if (country == null)
                throw new EntityNotFoundException(nameof(Country), request.Id);
            country.CountryName = request.CountryName;
            country.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CountryViewModel>(country);
        }
    }
}
