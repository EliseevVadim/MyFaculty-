using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairInfo
{
    public class GetPairInfoQueryHandler : IRequestHandler<GetPairInfoQuery, PairViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetPairInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairViewModel> Handle(GetPairInfoQuery request, CancellationToken cancellationToken)
        {
            Pair pair = await _context.Pairs
                .Include(pair => pair.Auditorium)
                    .ThenInclude(auditorium => auditorium.Floor.Faculty)
                .Include(pair => pair.Group)
                    .ThenInclude(group => group.Course.Faculty)
                .FirstOrDefaultAsync(pair => pair.Id == request.Id, cancellationToken);
            if (pair == null)
                throw new EntityNotFoundException(nameof(Pair), request.Id);
            return _mapper.Map<Pair, PairViewModel>(pair);
        }
    }
}
