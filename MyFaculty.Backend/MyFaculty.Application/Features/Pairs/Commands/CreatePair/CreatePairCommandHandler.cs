using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Pairs.Commands.CreatePair
{
    public class CreatePairCommandHandler : IRequestHandler<CreatePairCommand, PairViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreatePairCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairViewModel> Handle(CreatePairCommand request, CancellationToken cancellationToken)
        {
            Pair pair = new Pair()
            {
                PairName = request.PairName,
                PairInfoId = request.PairInfoId,
                DayOfWeekId = request.DayOfWeekId,
                DisciplineId = request.DisciplineId,
                TeacherId = request.TeacherId,
                AuditoriumId = request.AuditoriumId,
                PairRepeatingId = request.PairRepeatingId,
                GroupId = request.GroupId,
                Created = DateTime.Now
            };
            await _context.Pairs.AddAsync(pair, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PairViewModel>(pair);
        }
    }
}
