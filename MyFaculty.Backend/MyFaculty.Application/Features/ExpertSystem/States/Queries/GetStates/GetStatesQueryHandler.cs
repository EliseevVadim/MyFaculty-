using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto.ExpertSystemDto;
using MyFaculty.Application.ViewModels.ExpertSystemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ExpertSystem.States.Queries.GetStates
{
    public class GetStatesQueryHandler : IRequestHandler<GetStatesQuery, StatesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetStatesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StatesListViewModel> Handle(GetStatesQuery request, CancellationToken cancellationToken)
        {
            var states = await _context.ExpertSystemStates
                   .Include(state => state.Answers)
                   .ProjectTo<StateLookupDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);
            return new StatesListViewModel()
            {
                States = states
            };
        }
    }
}
