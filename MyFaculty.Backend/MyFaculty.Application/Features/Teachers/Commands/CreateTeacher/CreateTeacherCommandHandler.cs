using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, TeacherViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateTeacherCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherViewModel> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            Teacher teacher = new Teacher()
            {
                FIO = request.FIO,
                PhotoPath = request.PhotoPath,
                Email = request.Email,
                BirthDate = request.BirthDate,
                VerifiactionToken = Guid.NewGuid(),
                ScienceRankId = request.ScienceRankId,
                Created = DateTime.Now
            };
            await _context.Teachers.AddAsync(teacher, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TeacherViewModel>(teacher);
        }
    }
}
