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

namespace MyFaculty.Application.Features.PairRepeatings.Queries.GetPairRepeatings
{
    public class GetPairRepeatingsQueryHandler : IRequestHandler<GetPairRepeatingsQuery, PairRepeatingsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetPairRepeatingsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairRepeatingsListViewModel> Handle(GetPairRepeatingsQuery request, CancellationToken cancellationToken)
        {
            var repeatings = await _context.PairRepeatings
                .ProjectTo<PairRepeatingLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new PairRepeatingsListViewModel()
            {
                PairRepeatings = repeatings
            };
        }
    }
}
