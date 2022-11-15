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

namespace MyFaculty.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetUsersQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UsersListViewModel> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new UsersListViewModel()
            {
                Users = users
            };
        }
    }
}
