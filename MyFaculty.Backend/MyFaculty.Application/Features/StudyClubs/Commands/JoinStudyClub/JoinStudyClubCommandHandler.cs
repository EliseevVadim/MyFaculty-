using MediatR;
using MyFaculty.Application.Common.Interfaces;
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
