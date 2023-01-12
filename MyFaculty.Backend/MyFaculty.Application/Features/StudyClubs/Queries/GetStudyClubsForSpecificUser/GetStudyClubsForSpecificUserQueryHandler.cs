using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsForSpecificUser
{
    public class GetStudyClubsForSpecificUserQueryHandler : IRequestHandler<GetStudyClubsForSpecificUserQuery, StudyClubsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetStudyClubsForSpecificUserQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudyClubsListViewModel> Handle(GetStudyClubsForSpecificUserQuery request, CancellationToken cancellationToken)
        {
            AppUser requestedUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            if (requestedUser == null)
                throw new EntityNotFoundException(nameof(AppUser), request.UserId);
            var clubs = await _context.StudyClubs
                .Where(club => club.Members.Contains(requestedUser))
                .ProjectTo<StudyClubLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new StudyClubsListViewModel
            {
                StudyClubs = clubs
            };
        }
    }
}
