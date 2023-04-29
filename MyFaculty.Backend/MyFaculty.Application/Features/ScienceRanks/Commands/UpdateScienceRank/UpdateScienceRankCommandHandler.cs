using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank
{
    public class UpdateScienceRankCommandHandler : IRequestHandler<UpdateScienceRankCommand, ScienceRankViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateScienceRankCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScienceRankViewModel> Handle(UpdateScienceRankCommand request, CancellationToken cancellationToken)
        {
            ScienceRank rank = await _context.ScienceRanks
                .FirstOrDefaultAsync(rank => rank.Id == request.Id, cancellationToken);
            if (rank == null)
                throw new EntityNotFoundException(nameof(ScienceRank), request.Id);
            rank.RankName = request.RankName;
            rank.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ScienceRankViewModel>(rank);
        }
    }
}
