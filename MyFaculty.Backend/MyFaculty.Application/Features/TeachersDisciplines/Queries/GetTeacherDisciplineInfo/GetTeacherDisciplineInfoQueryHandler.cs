using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeacherDisciplineInfo
{
    public class GetTeacherDisciplineInfoQueryHandler : IRequestHandler<GetTeacherDisciplineInfoQuery, TeacherDisciplineViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetTeacherDisciplineInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherDisciplineViewModel> Handle(GetTeacherDisciplineInfoQuery request, CancellationToken cancellationToken)
        {
            TeacherDiscipline teacherDiscipline = await _context.TeacherDisciplines.FirstOrDefaultAsync(td => td.Id == request.Id, cancellationToken);
            if (teacherDiscipline == null)
                throw new EntityNotFoundException(nameof(TeacherDiscipline), request.Id);
            return _mapper.Map<TeacherDisciplineViewModel>(teacherDiscipline);
        }
    }
}
