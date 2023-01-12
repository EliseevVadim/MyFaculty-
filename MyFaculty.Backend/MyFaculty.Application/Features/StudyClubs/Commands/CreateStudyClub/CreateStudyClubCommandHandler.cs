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

namespace MyFaculty.Application.Features.StudyClubs.Commands.CreateStudyClub
{
    public class CreateStudyClubCommandHandler : IRequestHandler<CreateStudyClubCommand, StudyClubViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateStudyClubCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudyClubViewModel> Handle(CreateStudyClubCommand request, CancellationToken cancellationToken)
        {
            AppUser owner = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.OwnerId);
            if (owner == null)
                throw new EntityNotFoundException(nameof(AppUser), request.OwnerId);
            StudyClub club = new StudyClub
            {
                ClubName = request.StudyClubName,
                Description = request.Description,
                ImagePath = request.ImagePath,
                OwnerId = request.OwnerId,
                Created = DateTime.Now
            };
            club.Moderators.Add(owner);
            club.Members.Add(owner);
            await _context.StudyClubs.AddAsync(club, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<StudyClubViewModel>(club);
        }
    }
}
