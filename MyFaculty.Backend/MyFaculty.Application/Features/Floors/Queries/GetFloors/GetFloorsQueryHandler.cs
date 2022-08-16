using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using MyFaculty.Application.ViewModels;
using System.Threading;
using MyFaculty.Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using MyFaculty.Application.Dto;
using Microsoft.EntityFrameworkCore;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloors
{
    public class GetFloorsQueryHandler : IRequestHandler<GetFloorsQuery, FloorsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetFloorsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FloorsListViewModel> Handle(GetFloorsQuery request, CancellationToken cancellationToken)
        {
            var floors = await _context.Floors
                .ProjectTo<FloorLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new FloorsListViewModel()
            {
                Floors = floors
            };
        }
    }
}
