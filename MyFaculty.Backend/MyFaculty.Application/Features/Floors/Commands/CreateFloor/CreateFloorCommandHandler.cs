using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Floors.Commands.CreateFloor
{
    public class CreateFloorCommandHandler : IRequestHandler<CreateFloorCommand, FloorViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateFloorCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FloorViewModel> Handle(CreateFloorCommand request, CancellationToken cancellationToken)
        {
            Floor floor = new Floor
            {
                Name = request.Name,
                Bounds = request.Bounds,
                Created = DateTime.Now
            };
            await _context.Floors.AddAsync(floor);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<FloorViewModel>(floor);
        }
    }
}
