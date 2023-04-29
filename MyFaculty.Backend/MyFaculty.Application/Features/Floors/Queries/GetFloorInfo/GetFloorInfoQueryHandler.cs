using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

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
            Floor floor = await _context
                .Floors
                .Include(floor => floor.Auditoriums)
                .Include(floor => floor.SecondaryObjects)
                .ThenInclude(secondaryObject => secondaryObject.SecondaryObjectType)
                .FirstOrDefaultAsync(floor => floor.Id == request.Id, cancellationToken);
            if (floor == null)
                throw new EntityNotFoundException(nameof(Floor), request.Id);
            return _mapper.Map<FloorViewModel>(floor);
        }
    }
}
