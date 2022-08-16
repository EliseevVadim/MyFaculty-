using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeachersDisciplines
{
    public class GetTeachersDisciplinesQueryHandler : IRequestHandler<GetTeachersDisciplinesQuery, TeachersDisciplinesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetTeachersDisciplinesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeachersDisciplinesListViewModel> Handle(GetTeachersDisciplinesQuery request, CancellationToken cancellationToken)
        {
            var teachersDisciplines = await _context.TeacherDisciplines
                .ProjectTo<TeacherDisciplineLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new TeachersDisciplinesListViewModel()
            {
                TeacherDisciplines = teachersDisciplines
            };
        }
    }
}
