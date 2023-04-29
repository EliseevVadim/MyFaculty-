using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.CreateInfoPost
{
    public class CreateInfoPostCommandHandler : IRequestHandler<CreateInfoPostCommand, InfoPostViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateInfoPostCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostViewModel> Handle(CreateInfoPostCommand request, CancellationToken cancellationToken)
        {
            if (request.StudyClubId != null)
            {
                StudyClub studyClub = await _context.StudyClubs
                    .Include(club => club.Moderators)
                    .FirstOrDefaultAsync(club => club.Id == request.StudyClubId);
                if (studyClub == null)
                    throw new EntityNotFoundException(nameof(StudyClub), request.StudyClubId);
                if (!studyClub.Moderators.Any(user => user.Id == request.AuthorId))
                    throw new UnauthorizedAccessException("Данное действие Вам запрещено.");
            }
            else
            {
                InformationPublic informationPublic = await _context.InformationPublics
                    .Include(infoPublic => infoPublic.Moderators)
                    .FirstOrDefaultAsync(infoPublic => infoPublic.Id == request.InfoPublicId);
                if (informationPublic == null)
                    throw new EntityNotFoundException(nameof(InformationPublic), request.InfoPublicId);
                if (informationPublic.IsBanned)
                    throw new DestructiveActionException("Вы не можете выложить запись в это сообщество, поскольку оно заблокировано.");
                if (!informationPublic.Moderators.Any(user => user.Id == request.AuthorId))
                    throw new UnauthorizedAccessException("Данное действие Вам запрещено.");
            }
            InfoPost infoPost = new InfoPost()
            {
                TextContent = request.TextContent,
                Attachments = request.Attachments,
                PostAttachmentsUid = request.PostAttachmentsUid,
                AuthorId = request.AuthorId,
                StudyClubId = request.StudyClubId,
                InfoPublicId = request.InfoPublicId,
                CommentsAllowed = request.CommentsAllowed,
                Created = DateTime.Now
            };
            await _context.InfoPosts.AddAsync(infoPost, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<InfoPostViewModel>(infoPost);
        }
    }
}
