using AutoMapper;
using MyFaculty.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFaculty.Application.ViewModels;
using System.Threading;
using MyFaculty.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;

namespace MyFaculty.Application.Features.Pairs.Queries.GetPairInfo
{
    public class GetPairInfoQueryHandler : IRequestHandler<GetPairInfoQuery, PairViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
