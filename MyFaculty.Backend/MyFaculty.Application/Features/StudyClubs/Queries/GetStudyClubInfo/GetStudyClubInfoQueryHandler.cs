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

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubInfo
{
    public class GetStudyClubInfoQueryHandler : IRequestHandler<GetStudyClubInfoQuery, StudyClubViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetStudyClubInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudyClubViewModel> Handle(GetStudyClubInfoQuery request, CancellationToken cancellationToken)
        {
            StudyClub club = await _context.StudyClubs
                .Include(club => club.Members)
                .Include(club => club.Moderators)
                .FirstOrDefaultAsync(club => club.Id == request.Id, cancellationToken);
            if (club == null)
                throw new EntityNotFoundException(nameof(StudyClub), request.Id);
            return _mapper.Map<StudyClubViewModel>(club);
        }
    }
}
