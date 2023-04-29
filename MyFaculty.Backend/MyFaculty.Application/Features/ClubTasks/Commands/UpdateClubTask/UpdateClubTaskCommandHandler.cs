using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Commands.UpdateClubTask
{
    public class UpdateClubTaskCommandHandler : IRequestHandler<UpdateClubTaskCommand, ClubTaskViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateClubTaskCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTaskViewModel> Handle(UpdateClubTaskCommand request, CancellationToken cancellationToken)
        {
            ClubTask updatingTask = await _context.ClubTasks.FirstOrDefaultAsync(task => task.Id == request.TaskId, cancellationToken);
            if (updatingTask == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.TaskId);
            if (updatingTask.AuthorId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            updatingTask.TextContent = request.TextContent;
            updatingTask.Attachments = request.Attachments;
            updatingTask.DeadLine = request.DeadLine;
            updatingTask.Cost = request.Cost;
            updatingTask.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ClubTaskViewModel>(updatingTask);
        }
    }
}