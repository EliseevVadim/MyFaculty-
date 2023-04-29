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

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairsForGroup
{
    public class GetPairsForGroupQueryHandler : IRequestHandler<GetPairsForGroupQuery, PairsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetPairsForGroupQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairsListViewModel> Handle(GetPairsForGroupQuery request, CancellationToken cancellationToken)
        {
            var pairs = await _context.Pairs
                .Where(pair => pair.GroupId == request.GroupId)
                .ProjectTo<PairLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new PairsListViewModel()
            {
                Pairs = pairs
            };
        }
    }
}
