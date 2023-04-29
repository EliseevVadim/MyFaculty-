using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloorsForFaculty
{
    public class GetFloorsForFacultyQueryHandler : IRequestHandler<GetFloorsForFacultyQuery, FloorsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetFloorsForFacultyQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FloorsListViewModel> Handle(GetFloorsForFacultyQuery request, CancellationToken cancellationToken)
        {
            var floors = await _context.Floors
                .Where(floor => floor.FacultyId == request.FacultyId)
                .ProjectTo<FloorLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new FloorsListViewModel()
            {
                Floors = floors
            };
        }
    }
}
