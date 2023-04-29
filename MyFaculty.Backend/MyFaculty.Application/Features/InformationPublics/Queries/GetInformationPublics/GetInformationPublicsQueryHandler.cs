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

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublics
{
    public class GetInformationPublicsQueryHandler : IRequestHandler<GetInformationPublicsQuery, InformationPublicsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetInformationPublicsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InformationPublicsListViewModel> Handle(GetInformationPublicsQuery request, CancellationToken cancellationToken)
        {
            var infoPublics = await _context.InformationPublics
                .Where(infoPublic => !infoPublic.IsBanned)
                .ProjectTo<InformationPublicLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new InformationPublicsListViewModel
            {
                InformationPublics = infoPublics
            };
        }
    }
}
