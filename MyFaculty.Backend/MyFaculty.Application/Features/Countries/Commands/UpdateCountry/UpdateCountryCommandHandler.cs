using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, CountryViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
