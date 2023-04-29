using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Notifications.Queries.GetNotificationsForUser
{
    public class GetNotificationsForUserQueryHandler : IRequestHandler<GetNotificationsForUserQuery, NotificationsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetNotificationsForUserQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<NotificationsListViewModel> Handle(GetNotificationsForUserQuery request, CancellationToken cancellationToken)
        {
            var notifications = await _context.Notifications
                .Where(notification => notification.UserId == request.UserId)
                .OrderByDescending(notification => notification.Created)
                .ProjectTo<NotificationViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new NotificationsListViewModel()
            {
                Notifications = notifications
            };
        }
    }
}
