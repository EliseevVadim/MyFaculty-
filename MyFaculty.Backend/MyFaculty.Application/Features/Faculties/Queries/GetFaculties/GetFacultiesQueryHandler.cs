using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Queries.GetFaculties
{
    public class GetFacultiesQueryHandler : IRequestHandler<GetFacultiesQuery, FacultiesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetFacultiesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FacultiesListViewModel> Handle(GetFacultiesQuery request, CancellationToken cancellationToken)
        {
            var faculties = await _context.Faculties
                .ProjectTo<FacultyLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new FacultiesListViewModel()
            {
                Faculties = faculties
            };
        }
    }
}
