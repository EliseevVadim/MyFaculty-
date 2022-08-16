using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.PairInfos.Queries.GetPairInfos
{
    public class GetPairInfosQueryHandler : IRequestHandler<GetPairInfosQuery, PairInfosListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
