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

namespace MyFaculty.Application.Features.InfoPosts.Commands.DeleteInfoPost
{
    public class DeleteInfoPostCommandHandler : IRequestHandler<DeleteInfoPostCommand, InfoPostViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public DeleteInfoPostCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostViewModel> Handle(DeleteInfoPostCommand request, CancellationToken cancellationToken)
        {
            InfoPost infoPost = await _context.InfoPosts
                .Include(infoPost => infoPost.OwningInformationPublic)
                .Include(infoPost => infoPost.OwningStudyClub)
                .FirstOrDefaultAsync(infoPost => infoPost.Id == request.Id);
            if (infoPost == null)
                throw new EntityNotFoundException(nameof(InfoPost), request.Id);
            if (infoPost.AuthorId != request.IssuerId &&
                   ((infoPost.OwningInformationPublic != null && infoPost.OwningInformationPublic.OwnerId != request.IssuerId) ||
                   (infoPost.OwningStudyClub != null && infoPost.OwningStudyClub.OwnerId != request.IssuerId)))
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            _context.InfoPosts.Remove(infoPost);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<InfoPostViewModel>(infoPost);
        }
    }
}
