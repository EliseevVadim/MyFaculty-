using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.AdministrativeReports.Queries.GetUsersBansReports
{
    public class GetUsersBansReportsQueryHandler : IRequestHandler<GetUsersBansReportsQuery, UsersBansReportsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetUsersBansReportsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersBansReportsListViewModel> Handle(GetUsersBansReportsQuery request, CancellationToken cancellationToken)
        {
            var banReports = await _context.UsersBansReports
                .OrderByDescending(banReport => banReport.Created)
                .ProjectTo<UserBanReportViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new UsersBansReportsListViewModel()
            {
                UsersBansReports = banReports
            };
        }
    }
}
