using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using System.Threading.Tasks;
using MyFaculty.Domain.Entities;
using System.Threading;
using MyFaculty.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.ViewModels;
using AutoMapper;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank
{
    public class UpdateScienceRankCommandHandler : IRequestHandler<UpdateScienceRankCommand, ScienceRankViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
