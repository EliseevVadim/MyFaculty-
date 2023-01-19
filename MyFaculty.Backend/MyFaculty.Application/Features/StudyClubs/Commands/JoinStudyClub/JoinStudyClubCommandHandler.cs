using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Commands.JoinStudyClub
{
    public class JoinStudyClubCommandHandler : IRequestHandler<JoinStudyClubCommand>
    {
        private IMFDbContext _context;

        public JoinStudyClubCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(JoinStudyClubCommand request, CancellationToken cancellationToken)
        {
            string query = $"INSERT INTO usersatstudyclubs (MembersId, StudyClubsId) VALUES ({request.UserId}, {request.StudyClubId})";
            await _context.ExecuteSqlRawAsync(query);
            return Unit.Value;
        }
    }
}
