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


namespace MyFaculty.Application.Features.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, CityViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateCityCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CityViewModel> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            City city = await _context.Cities.FirstOrDefaultAsync(city => city.Id == request.Id);
            if (city == null)
                throw new EntityNotFoundException(nameof(City), request.Id);
            city.CityName = request.CityName;
            city.RegionId = request.RegionId;
            city.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<CityViewModel>(city);
        }
    }
}
