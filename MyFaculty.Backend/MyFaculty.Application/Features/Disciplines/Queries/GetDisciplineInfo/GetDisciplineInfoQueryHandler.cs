using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Disciplines.Queries.GetDisciplineInfo
{
    public class GetDisciplineInfoQueryHandler : IRequestHandler<GetDisciplineInfoQuery, DisciplineViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetDisciplineInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DisciplineViewModel> Handle(GetDisciplineInfoQuery request, CancellationToken cancellationToken)
        {
            Discipline discipline = await _context.Disciplines.FirstOrDefaultAsync(discipline => discipline.Id == request.Id, cancellationToken);
            if (discipline == null)
                throw new EntityNotFoundException(nameof(Discipline), request.Id);
            return _mapper.Map<DisciplineViewModel>(discipline);
        }
    }
}
