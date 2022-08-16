using AutoMapper;
using MyFaculty.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFaculty.Application.ViewModels;
using System.Threading;
using MyFaculty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorInfo
{
    public class GetFloorInfoQueryHandler : IRequestHandler<GetFloorInfoQuery, FloorViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetFloorInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FloorViewModel> Handle(GetFloorInfoQuery request, CancellationToken cancellationToken)
        {
            Floor floor = await _context.Floors.FirstOrDefaultAsync(floor => floor.Id == request.Id, cancellationToken);
            if (floor == null)
                throw new EntityNotFoundException(nameof(Floor), request.Id);
            return _mapper.Map<FloorViewModel>(floor);
        }
    }
}
