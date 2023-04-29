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

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.UpdateTeacherDiscipline
{
    public class UpdateTeacherDisciplineCommandHandler : IRequestHandler<UpdateTeacherDisciplineCommand, TeacherDisciplineViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTeacherDisciplineCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherDisciplineViewModel> Handle(UpdateTeacherDisciplineCommand request, CancellationToken cancellationToken)
        {
            TeacherDiscipline teacherDiscipline = await _context.TeacherDisciplines.FirstOrDefaultAsync(td => td.Id == request.Id, cancellationToken);
            if (teacherDiscipline == null)
                throw new EntityNotFoundException(nameof(TeacherDiscipline), request.Id);
            teacherDiscipline.TeacherId = request.TeacherId;
            teacherDiscipline.DisciplineId = request.DisciplineId;
            teacherDiscipline.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TeacherDisciplineViewModel>(teacherDiscipline);
        }
    }
}
