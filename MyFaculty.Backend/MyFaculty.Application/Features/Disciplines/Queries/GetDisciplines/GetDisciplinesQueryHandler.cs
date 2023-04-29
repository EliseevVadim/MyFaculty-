using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Disciplines.Queries.GetDisciplines
{
    public class GetDisciplinesQueryHandler : IRequestHandler<GetDisciplinesQuery, DisciplinesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetDisciplinesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DisciplinesListViewModel> Handle(GetDisciplinesQuery request, CancellationToken cancellationToken)
        {
            var disciplines = await _context.Disciplines
                .ProjectTo<DisciplineLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new DisciplinesListViewModel()
            {
                Disciplines = disciplines
            };
        }
    }
}
