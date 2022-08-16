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

namespace MyFaculty.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Teacher>
    {
        private IMFDbContext _context;

        public UpdateTeacherCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.Id == request.Id, cancellationToken);
            if (teacher == null)
                throw new EntityNotFoundException(nameof(Teacher), request.Id);
            teacher.FIO = request.FIO;
            teacher.PhotoPath = request.PhotoPath;
            teacher.BirthDate = request.BirthDate;
            teacher.Email = request.Email;
            teacher.ScienceRankId = request.ScienceRankId;
            teacher.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return teacher;
        }
    }
}
