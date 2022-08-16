using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, Teacher>
    {
        private IMFDbContext _context;

        public CreateTeacherCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Teacher> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            Teacher teacher = new Teacher()
            {
                FIO = request.FIO,
                PhotoPath = request.PhotoPath,
                Email = request.Email,
                BirthDate = request.BirthDate,
                ScienceRankId = request.ScienceRankId,
                Created = DateTime.Now
            };
            await _context.Teachers.AddAsync(teacher, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return teacher;
        }
    }
}
