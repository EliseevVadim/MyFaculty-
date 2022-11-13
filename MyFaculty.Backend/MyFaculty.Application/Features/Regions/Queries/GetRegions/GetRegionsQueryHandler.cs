using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Queries.GetRegions
{
    public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, RegionsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetRegionsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RegionsListViewModel> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
        {
            var regions = await _context.Regions
                .ProjectTo<RegionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new RegionsListViewModel()
            {
                Regions = regions
            };
        }
    }
}
