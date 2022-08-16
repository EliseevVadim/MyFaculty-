using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Auditoriums.Commands.UpdateAuditorium
{
    public class UpdateAuditoriumCommandHandler : IRequestHandler<UpdateAuditoriumCommand, Auditorium>
    {
        private IMFDbContext _context;

        public UpdateAuditoriumCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Auditorium> Handle(UpdateAuditoriumCommand request, CancellationToken cancellationToken)
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
            return auditorium;
        }
    }
}
