using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.ViewModels;
using AutoMapper;

namespace MyFaculty.Application.Features.Floors.Commands.UpdateFloor
{
    public class UpdateFloorCommandHandler : IRequestHandler<UpdateFloorCommand, FloorViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
