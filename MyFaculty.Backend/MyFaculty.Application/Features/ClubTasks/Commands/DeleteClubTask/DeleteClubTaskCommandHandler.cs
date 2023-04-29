using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Commands.DeleteClubTask
{
    public class DeleteClubTaskCommandHandler : IRequestHandler<DeleteClubTaskCommand, ClubTaskViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public DeleteClubTaskCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTaskViewModel> Handle(DeleteClubTaskCommand request, CancellationToken cancellationToken)
        {
            ClubTask task = await _context.ClubTasks
                .Include(task => task.OwningStudyClub)
                .FirstOrDefaultAsync(task => task.Id == request.Id);
            if (task == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.Id);
            if (task.AuthorId != request.IssuerId && task.OwningStudyClub.OwnerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            _context.ClubTasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ClubTaskViewModel>(task);
        }
    }
}
