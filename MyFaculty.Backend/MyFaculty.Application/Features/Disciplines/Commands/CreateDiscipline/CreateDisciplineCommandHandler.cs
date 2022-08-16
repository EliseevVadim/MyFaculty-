using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline
{
    public class CreateDisciplineCommandHandler : IRequestHandler<CreateDisciplineCommand, Discipline>
    {
        private IMFDbContext _context;

        public CreateDisciplineCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Discipline> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
        {
            Discipline discipline = new Discipline()
            {
                DisciplineName = request.DisciplineName,
                Created = DateTime.Now
            };
            await _context.Disciplines.AddAsync(discipline, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return discipline;
        }
    }
}
