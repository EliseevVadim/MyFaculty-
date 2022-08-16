using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using System.Threading.Tasks;
using MyFaculty.Application.ViewModels;
using System.Threading;
using MyFaculty.Application.Common.Interfaces;
using AutoMapper;
using MyFaculty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;

namespace MyFaculty.Application.Features.ScienceRanks.Queries.GetScienceRankInfo
{
    public class GetScienceRankInfoQueryHandler : IRequestHandler<GetScienceRankInfoQuery, ScienceRankViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetScienceRankInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScienceRankViewModel> Handle(GetScienceRankInfoQuery request, CancellationToken cancellationToken)
        {
            ScienceRank rank = await _context.ScienceRanks.FirstOrDefaultAsync(rank => rank.Id == request.Id);
            if (rank == null)
                throw new EntityNotFoundException(nameof(ScienceRank), request.Id);
            return _mapper.Map<ScienceRankViewModel>(rank);
        }
    }
}
