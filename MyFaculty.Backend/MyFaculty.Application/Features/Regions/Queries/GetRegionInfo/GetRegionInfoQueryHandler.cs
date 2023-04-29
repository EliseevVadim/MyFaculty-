using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegionInfo
{
    public class GetRegionInfoQueryHandler : IRequestHandler<GetRegionInfoQuery, RegionViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetRegionInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegionViewModel> Handle(GetRegionInfoQuery request, CancellationToken cancellationToken)
        {
            Region region = await _context.Regions.FirstOrDefaultAsync(region => region.Id == request.Id);
            if (region == null)
                throw new EntityNotFoundException(nameof(Region), request.Id);
            return _mapper.Map<RegionViewModel>(region);
        }
    }
}
