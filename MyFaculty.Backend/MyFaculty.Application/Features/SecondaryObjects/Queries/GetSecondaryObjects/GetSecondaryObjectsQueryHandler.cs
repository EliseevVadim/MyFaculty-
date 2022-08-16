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

namespace MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjects
{
    public class GetSecondaryObjectsQueryHandler : IRequestHandler<GetSecondaryObjectsQuery, SecondaryObjectsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetSecondaryObjectsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectsListViewModel> Handle(GetSecondaryObjectsQuery request, CancellationToken cancellationToken)
        {
            var secondaryObjects = await _context.SecondaryObjects
                .ProjectTo<SecondaryObjectLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new SecondaryObjectsListViewModel()
            {
                SecondaryObjects = secondaryObjects
            };
        }
    }
}
