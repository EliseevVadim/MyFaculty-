using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline
{
    public class CreateTeacherDisciplineCommandHandler : IRequestHandler<CreateTeacherDisciplineCommand, TeacherDiscipline>
    {
        private IMFDbContext _context;

        public CreateTeacherDisciplineCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherDiscipline> Handle(CreateTeacherDisciplineCommand request, CancellationToken cancellationToken)
        {
            TeacherDiscipline teacherDiscipline = new TeacherDiscipline()
            {
                TeacherId = request.TeacherId,
                DisciplineId = request.DisciplineId,
                Created = DateTime.Now
            };
            await _context.TeacherDisciplines.AddAsync(teacherDiscipline, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return teacherDiscipline;
        }
    }
}
