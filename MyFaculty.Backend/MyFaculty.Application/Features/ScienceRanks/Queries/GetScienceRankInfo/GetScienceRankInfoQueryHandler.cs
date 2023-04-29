using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

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
