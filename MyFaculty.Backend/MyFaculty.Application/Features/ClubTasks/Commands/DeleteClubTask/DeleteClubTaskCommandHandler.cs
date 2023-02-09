using AutoMapper;
using MediatR;
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

namespace MyFaculty.Application.Features.ClubTasks.Commands.DeleteClubTask
{
    public class DeleteClubTaskCommandHandler : IRequestHandler<DeleteClubTaskCommand, ClubTaskViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public DeleteClubTaskCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTaskViewModel> Handle(DeleteClubTaskCommand request, CancellationToken cancellationToken)
        {
            ClubTask task = await _context.ClubTasks.FindAsync(new object[] { request.Id }, cancellationToken);
            if (task == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.Id);
            if (task.AuthorId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            _context.ClubTasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ClubTaskViewModel>(task);
        }
    }
}
