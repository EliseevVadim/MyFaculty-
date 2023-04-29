using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloors
{
    public class GetFloorsQueryHandler : IRequestHandler<GetFloorsQuery, FloorsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetFloorsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FloorsListViewModel> Handle(GetFloorsQuery request, CancellationToken cancellationToken)
        {
            var floors = await _context.Floors
                .ProjectTo<FloorLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new FloorsListViewModel()
            {
                Floors = floors
            };
        }
    }
}
