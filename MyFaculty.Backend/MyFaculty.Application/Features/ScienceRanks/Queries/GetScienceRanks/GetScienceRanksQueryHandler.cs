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

namespace MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRanks
{
    public class GetScienceRanksQueryHandler : IRequestHandler<GetScienceRanksQuery, ScienceRanksListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
