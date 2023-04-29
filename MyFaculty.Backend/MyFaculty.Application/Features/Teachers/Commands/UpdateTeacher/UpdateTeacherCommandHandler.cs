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

namespace MyFaculty.Application.Features.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, TeacherViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateTeacherCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherViewModel> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.Id == request.Id, cancellationToken);
            if (teacher == null)
                throw new EntityNotFoundException(nameof(Teacher), request.Id);
            teacher.FIO = request.FIO;
            teacher.PhotoPath = String.IsNullOrEmpty(request.PhotoPath) ? teacher.PhotoPath : request.PhotoPath;
            teacher.BirthDate = request.BirthDate;
            teacher.Email = request.Email;
            teacher.ScienceRankId = request.ScienceRankId;
            teacher.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<TeacherViewModel>(teacher);
        }
    }
}
