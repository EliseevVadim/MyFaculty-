using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRanks
{
    public class GetScienceRanksQueryHandler : IRequestHandler<GetScienceRanksQuery, ScienceRanksListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetScienceRanksQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScienceRanksListViewModel> Handle(GetScienceRanksQuery request, CancellationToken cancellationToken)
        {
            var scienceRanks = await _context.ScienceRanks
                .ProjectTo<ScienceRankLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new ScienceRanksListViewModel()
            {
                ScienceRanks = scienceRanks
            };
        }
    }
}
