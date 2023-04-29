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

namespace MyFaculty.Application.Features.Auditoriums.Commands.UpdateAuditorium
{
    public class UpdateAuditoriumCommandHandler : IRequestHandler<UpdateAuditoriumCommand, AuditoriumViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateAuditoriumCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuditoriumViewModel> Handle(UpdateAuditoriumCommand request, CancellationToken cancellationToken)
        {
            Auditorium auditorium = await _context.Auditoriums.FirstOrDefaultAsync(auditorium => auditorium.Id == request.Id, cancellationToken);
            if (auditorium == null)
                throw new EntityNotFoundException(nameof(Auditorium), request.Id);
            auditorium.TeacherId = request.TeacherId;
            auditorium.FloorId = request.FloorId;
            auditorium.PositionInfo = request.PositionInfo;
            auditorium.AuditoriumName = request.AuditoriumName;
            auditorium.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AuditoriumViewModel>(auditorium);
        }
    }
}
