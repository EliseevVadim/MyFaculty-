using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypes
{
    public class GetSecondaryObjectTypesQueryHandler : IRequestHandler<GetSecondaryObjectTypesQuery, SecondaryObjectTypesListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetSecondaryObjectTypesQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectTypesListViewModel> Handle(GetSecondaryObjectTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _context.SecondaryObjectTypes
                .ProjectTo<SecondaryObjectTypeLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new SecondaryObjectTypesListViewModel()
            {
                SecondaryObjectTypes = types
            };
        }
    }
}
