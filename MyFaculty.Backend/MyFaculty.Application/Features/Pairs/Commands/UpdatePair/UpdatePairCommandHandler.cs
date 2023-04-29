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

namespace MyFaculty.Application.Features.Pairs.Commands.UpdatePair
{
    public class UpdatePairCommandHandler : IRequestHandler<UpdatePairCommand, PairViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdatePairCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairViewModel> Handle(UpdatePairCommand request, CancellationToken cancellationToken)
        {
            Pair pair = await _context.Pairs.FirstOrDefaultAsync(pair => pair.Id == request.Id, cancellationToken);
            if (pair == null)
                throw new EntityNotFoundException(nameof(Pair), request.Id);
            pair.PairName = request.PairName;
            pair.PairInfoId = request.PairInfoId;
            pair.DayOfWeekId = request.DayOfWeekId;
            pair.DisciplineId = request.DisciplineId;
            pair.TeacherId = request.TeacherId;
            pair.AuditoriumId = request.AuditoriumId;
            pair.PairRepeatingId = request.PairRepeatingId;
            pair.GroupId = request.GroupId;
            pair.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<PairViewModel>(pair);
        }
    }
}
