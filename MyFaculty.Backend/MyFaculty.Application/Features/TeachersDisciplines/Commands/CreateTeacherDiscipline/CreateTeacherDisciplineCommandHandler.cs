using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TeachersDisciplines.Commands.CreateTeacherDiscipline
{
    public class CreateTeacherDisciplineCommandHandler : IRequestHandler<CreateTeacherDisciplineCommand, TeacherDisciplineViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateTeacherDisciplineCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherDisciplineViewModel> Handle(CreateTeacherDisciplineCommand request, CancellationToken cancellationToken)
        {
            TeacherDiscipline teacherDiscipline = new TeacherDiscipline()
            {
                TeacherId = request.TeacherId,
                DisciplineId = request.DisciplineId,
                Created = DateTime.Now
            };
            await _context.TeacherDisciplines.AddAsync(teacherDiscipline, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TeacherDisciplineViewModel>(teacherDiscipline);
        }
    }
}
