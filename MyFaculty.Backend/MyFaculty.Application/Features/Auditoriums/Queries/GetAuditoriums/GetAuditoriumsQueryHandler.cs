using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriums
{
    public class GetAuditoriumsQueryHandler : IRequestHandler<GetAuditoriumsQuery, AuditoriumsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetAuditoriumsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuditoriumsListViewModel> Handle(GetAuditoriumsQuery request, CancellationToken cancellationToken)
        {
            var auditoriums = await _context.Auditoriums
                .ProjectTo<AuditoriumLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new AuditoriumsListViewModel()
            {
                Auditoriums = auditoriums
            };
        }
    }
}
