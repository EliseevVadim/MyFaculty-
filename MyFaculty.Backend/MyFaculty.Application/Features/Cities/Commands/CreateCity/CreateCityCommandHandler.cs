using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public CreateCityCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityViewModel> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            City city = new City()
            {
                CityName = request.CityName,
                RegionId = request.RegionId,
                Created = DateTime.Now
            };
            await _context.Cities.AddAsync(city, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CityViewModel>(city);
        }
    }
}
