using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline
{
    public class CreateDisciplineCommandHandler : IRequestHandler<CreateDisciplineCommand, DisciplineViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateDisciplineCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DisciplineViewModel> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
        {
            Discipline discipline = new Discipline()
            {
                DisciplineName = request.DisciplineName,
                Created = DateTime.Now
            };
            await _context.Disciplines.AddAsync(discipline, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<DisciplineViewModel>(discipline);
        }
    }
}
