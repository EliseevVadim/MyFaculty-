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

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetBannedInformationPublics
{
    public class GetBannedInformationPublicsQueryHandler : IRequestHandler<GetBannedInformationPublicsQuery, InformationPublicsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetBannedInformationPublicsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InformationPublicsListViewModel> Handle(GetBannedInformationPublicsQuery request, CancellationToken cancellationToken)
        {
            var infoPublics = await _context.InformationPublics
                .Where(infoPublic => infoPublic.IsBanned)
                .ProjectTo<InformationPublicLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new InformationPublicsListViewModel
            {
                InformationPublics = infoPublics
            };
        }
    }
}
