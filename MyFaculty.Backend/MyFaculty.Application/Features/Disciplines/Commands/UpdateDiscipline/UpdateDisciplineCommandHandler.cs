using MyFaculty.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Domain.Entities;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;

namespace MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline
{
    public class UpdateDisciplineCommandHandler : IRequestHandler<UpdateDisciplineCommand, Discipline>
    {
        private IMFDbContext _context;

        public UpdateDisciplineCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Discipline> Handle(UpdateDisciplineCommand request, CancellationToken cancellationToken)
        {
            Discipline discipline = await _context.Disciplines.FirstOrDefaultAsync(discipline => discipline.Id == request.Id, cancellationToken);
            if (discipline == null)
                throw new EntityNotFoundException(nameof(Discipline), request.Id);
            discipline.DisciplineName = request.DisciplineName;
            discipline.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return discipline;
        }
    }
}
