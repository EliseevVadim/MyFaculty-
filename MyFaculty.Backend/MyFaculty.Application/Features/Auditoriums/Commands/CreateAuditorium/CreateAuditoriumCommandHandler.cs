using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium
{
    public class CreateAuditoriumCommandHandler : IRequestHandler<CreateAuditoriumCommand, Auditorium>
    {
        private IMFDbContext _context;

        public CreateAuditoriumCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Auditorium> Handle(CreateAuditoriumCommand request, CancellationToken cancellationToken)
        {
            Auditorium auditorium = new Auditorium()
            {
                AuditoriumName = request.AuditoriumName,
                PositionInfo = request.PositionInfo,
                FloorId = request.FloorId,
                TeacherId = request.TeacherId,
                Created = DateTime.Now
            };
            await _context.Auditoriums.AddAsync(auditorium, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return auditorium;
        }
    }
}
