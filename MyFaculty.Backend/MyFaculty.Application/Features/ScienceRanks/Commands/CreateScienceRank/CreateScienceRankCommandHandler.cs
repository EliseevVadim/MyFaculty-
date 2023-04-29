using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank
{
    public class CreateScienceRankCommandHandler : IRequestHandler<CreateScienceRankCommand, ScienceRankViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public CreateScienceRankCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ScienceRankViewModel> Handle(CreateScienceRankCommand request, CancellationToken cancellationToken)
        {
            ScienceRank rank = new ScienceRank
            {
                RankName = request.RankName,
                Created = DateTime.Now
            };
            await _context.ScienceRanks.AddAsync(rank);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ScienceRankViewModel>(rank);
        }
    }
}
