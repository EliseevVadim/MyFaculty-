using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Queries.GetTeacherInfo
{
    public class GetTeacherInfoQueryHandler : IRequestHandler<GetTeacherInfoQuery, TeacherViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetTeacherInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherViewModel> Handle(GetTeacherInfoQuery request, CancellationToken cancellationToken)
        {
            Teacher teacher = await _context
                .Teachers
                .Include(teacher => teacher.ScienceRank)
                .Include(teacher => teacher.TeacherDisciplines)
                    .ThenInclude(td => td.Discipline)
                .FirstOrDefaultAsync(teacher => teacher.Id == request.Id, cancellationToken);
            if (teacher == null)
                throw new EntityNotFoundException(nameof(Teacher), request.Id);
            return _mapper.Map<TeacherViewModel>(teacher);
        }
    }
}
