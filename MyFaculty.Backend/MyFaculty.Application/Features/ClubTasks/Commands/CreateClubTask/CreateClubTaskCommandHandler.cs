using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.ClubTasks.Commands.CreateClubTask
{
    public class CreateClubTaskCommandHandler : IRequestHandler<CreateClubTaskCommand, ClubTaskViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateClubTaskCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTaskViewModel> Handle(CreateClubTaskCommand request, CancellationToken cancellationToken)
        {
            ClubTask task = new ClubTask()
            {
                TextContent = request.TextContent,
                Attachments = request.Attachments,
                PostAttachmentsUid = request.PostAttachmentsUid,
                StudyClubId = request.StudyClubId,
                AuthorId = request.AuthorId,
                DeadLine = request.DeadLine,
                Cost = request.Cost,
                Created = DateTime.Now
            };
            await _context.ClubTasks.AddAsync(task, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<ClubTaskViewModel>(task);
        }
    }
}
