using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto.ExpertSystemDto;
using MyFaculty.Application.ViewModels.ExpertSystemViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ExpertSystem.StateTransitions.Queries.GetStateTransitions
{
    public class GetStateTransitionsQueryHandler : IRequestHandler<GetStateTransitionsQuery, StateTransitionsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetStateTransitionsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StateTransitionsListViewModel> Handle(GetStateTransitionsQuery request, CancellationToken cancellationToken)
        {
            var transitions = await _context.ExpertSystemStateTransitions
                .ProjectTo<StateTransitionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new StateTransitionsListViewModel()
            {
                StateTransitions = transitions
            };
        }
    }
}
