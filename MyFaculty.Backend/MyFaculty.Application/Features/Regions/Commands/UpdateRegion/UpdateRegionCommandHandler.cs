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

namespace MyFaculty.Application.Features.Regions.Commands.UpdateRegion
{
    public class UpdateRegionCommandHandler : IRequestHandler<UpdateRegionCommand, RegionViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateRegionCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegionViewModel> Handle(UpdateRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = await _context.Regions.FirstOrDefaultAsync(region => region.Id == request.Id, cancellationToken);
            if (region == null)
                throw new EntityNotFoundException(nameof(Region), request.Id);
            region.RegionName = request.RegionName;
            region.CountryId = request.CountryId;
            region.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<RegionViewModel>(region);
        }
    }
}
