using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.AdministrativeReports.Queries.GetInformationPublicsBansReports
{
    public class GetInformationPublicsBansReportsQueryHandler : IRequestHandler<GetInformationPublicsBansReportsQuery, InformationPublicsBansReportsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetInformationPublicsBansReportsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InformationPublicsBansReportsListViewModel> Handle(GetInformationPublicsBansReportsQuery request, CancellationToken cancellationToken)
        {
            var banReports = await _context.InformationPublicsBansReports
                .OrderByDescending(banReport => banReport.Created)
                .ProjectTo<InformationPublicBanReportViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new InformationPublicsBansReportsListViewModel()
            {
                PublicsBansReports = banReports
            };
        }
    }
}
