using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypes
{
    public class GetSecondaryObjectTypesQueryHandler : IRequestHandler<GetSecondaryObjectTypesQuery, SecondaryObjectTypesListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

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
