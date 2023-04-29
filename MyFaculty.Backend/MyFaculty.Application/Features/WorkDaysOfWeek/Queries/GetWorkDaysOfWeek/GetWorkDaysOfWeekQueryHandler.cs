using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.WorkDaysOfWeek.Queries.GetWorkDaysOfWeek
{
    public class GetWorkDaysOfWeekQueryHandler : IRequestHandler<GetWorkDaysOfWeekQuery, WorkDaysOfWeekListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetWorkDaysOfWeekQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WorkDaysOfWeekListViewModel> Handle(GetWorkDaysOfWeekQuery request, CancellationToken cancellationToken)
        {
            var days = await _context.DaysOfWeek
                .ProjectTo<WorkDayOfWeekLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new WorkDaysOfWeekListViewModel()
            {
                WorkDaysOfWeek = days
            };
        }
    }
}
