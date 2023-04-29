using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumsForFaculty
{
    public class GetAuditoriumsForFacultyQueryHandler : IRequestHandler<GetAuditoriumsForFacultyQuery, AuditoriumsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetAuditoriumsForFacultyQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuditoriumsListViewModel> Handle(GetAuditoriumsForFacultyQuery request, CancellationToken cancellationToken)
        {
            var auditoriums = await _context.Auditoriums
                .Where(auditorium => auditorium.Floor.FacultyId == request.FacultyId)
                .ProjectTo<AuditoriumLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new AuditoriumsListViewModel()
            {
                Auditoriums = auditoriums
            };
        }
    }
}
