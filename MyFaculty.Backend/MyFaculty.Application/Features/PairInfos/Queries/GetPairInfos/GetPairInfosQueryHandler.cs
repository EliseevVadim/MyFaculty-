using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.PairInfos.Queries.GetPairInfos
{
    public class GetPairInfosQueryHandler : IRequestHandler<GetPairInfosQuery, PairInfosListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetPairInfosQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairInfosListViewModel> Handle(GetPairInfosQuery request, CancellationToken cancellationToken)
        {
            var pairInfos = await _context.PairInfos
                .ProjectTo<PairInfoLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new PairInfosListViewModel()
            {
                PairInfos = pairInfos
            };
        }
    }
}
