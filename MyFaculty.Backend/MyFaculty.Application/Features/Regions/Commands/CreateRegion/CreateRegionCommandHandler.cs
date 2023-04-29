using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Commands.CreateRegion
{
    public class CreateRegionCommandHandler : IRequestHandler<CreateRegionCommand, RegionViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateRegionCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegionViewModel> Handle(CreateRegionCommand request, CancellationToken cancellationToken)
        {
            Region region = new Region()
            {
                RegionName = request.RegionName,
                CountryId = request.CountryId,
                Created = DateTime.Now
            };
            await _context.Regions.AddAsync(region, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<RegionViewModel>(region);
        }
    }
}
