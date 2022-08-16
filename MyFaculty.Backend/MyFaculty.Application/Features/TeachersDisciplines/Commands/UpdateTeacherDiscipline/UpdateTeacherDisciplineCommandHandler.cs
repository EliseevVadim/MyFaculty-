﻿using System;
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

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline
{
    public class UpdateTeacherDisciplineCommandHandler : IRequestHandler<UpdateTeacherDisciplineCommand, TeacherDiscipline>
    {
        private IMFDbContext _context;

        public UpdateTeacherDisciplineCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherDiscipline> Handle(UpdateTeacherDisciplineCommand request, CancellationToken cancellationToken)
        {
            TeacherDiscipline teacherDiscipline = await _context.TeacherDisciplines.FirstOrDefaultAsync(td => td.Id == request.Id, cancellationToken);
            if (teacherDiscipline == null)
                throw new EntityNotFoundException(nameof(TeacherDiscipline), request.Id);
            teacherDiscipline.TeacherId = request.TeacherId;
            teacherDiscipline.DisciplineId = request.DisciplineId;
            teacherDiscipline.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return teacherDiscipline;
        }
    }
}
