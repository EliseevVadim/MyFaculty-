using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicsByName
{
    public class GetInformationPublicsByNameQueryHandler : IRequestHandler<GetInformationPublicsByNameQuery, InformationPublicsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetInformationPublicsByNameQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InformationPublicsListViewModel> Handle(GetInformationPublicsByNameQuery request, CancellationToken cancellationToken)
        {
            var infoPublics = await _context.InformationPublics
                .Where(infoPublic => infoPublic.PublicName.Contains(request.SearchRequest) && !infoPublic.IsBanned)
                .ProjectTo<InformationPublicLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new InformationPublicsListViewModel
            {
                InformationPublics = infoPublics
            };
        }
    }
}
