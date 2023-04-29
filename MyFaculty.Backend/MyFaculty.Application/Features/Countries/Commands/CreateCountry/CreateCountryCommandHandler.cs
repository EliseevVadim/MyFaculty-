using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CountryViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateCountryCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CountryViewModel> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            Country country = new Country()
            {
                CountryName = request.CountryName,
                Created = DateTime.Now
            };
            await _context.Countries.AddAsync(country, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CountryViewModel>(country);
        }
    }
}
