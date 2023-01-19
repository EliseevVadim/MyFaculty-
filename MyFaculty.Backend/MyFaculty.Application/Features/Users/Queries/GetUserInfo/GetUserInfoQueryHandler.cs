using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Queries.GetUserInfo
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetUserInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _context.Users
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.Members)
                .Include(user => user.StudyClubs)
                    .ThenInclude(club => club.Moderators)
                .Include(user => user.City)
                    .ThenInclude(city => city.Region)
                        .ThenInclude(region => region.Country)
                .Include(user => user.Faculty)
                .Include(user => user.Course)
                .Include(user => user.Group)
                .FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);
            if (user == null)
                throw new EntityNotFoundException(nameof(AppUser), request.Id);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
