using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairs
{
    public class GetPairsQueryHandler : IRequestHandler<GetPairsQuery, PairsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetPairsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairsListViewModel> Handle(GetPairsQuery request, CancellationToken cancellationToken)
        {
            var pairs = await _context.Pairs
                .ProjectTo<PairLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new PairsListViewModel()
            {
                Pairs = pairs
            };
        }
    }
}
