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

namespace MyFaculty.Application.Features.Floors.Commands.UpdateFloor
{
    public class UpdateFloorCommandHandler : IRequestHandler<UpdateFloorCommand, FloorViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFloorCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FloorViewModel> Handle(UpdateFloorCommand request, CancellationToken cancellationToken)
        {
            Floor floor = await _context.Floors.FirstOrDefaultAsync(floor => floor.Id == request.Id, cancellationToken);
            if (floor == null)
                throw new EntityNotFoundException(nameof(Floor), request.Id);
            floor.Name = request.Name;
            floor.Bounds = request.Bounds;
            floor.FacultyId = request.FacultyId;
            floor.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<FloorViewModel>(floor);
        }
    }
}
