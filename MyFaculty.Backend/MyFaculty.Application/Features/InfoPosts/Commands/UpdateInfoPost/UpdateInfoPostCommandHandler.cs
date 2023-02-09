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

namespace MyFaculty.Application.Features.InfoPosts.Commands.UpdateInfoPost
{
    public class UpdateInfoPostCommandHandler : IRequestHandler<UpdateInfoPostCommand, InfoPostViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateInfoPostCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostViewModel> Handle(UpdateInfoPostCommand request, CancellationToken cancellationToken)
        {
            InfoPost updatingPost = await _context.InfoPosts.FirstOrDefaultAsync(infoPost => infoPost.Id == request.InfoPostId, cancellationToken);
            if (updatingPost == null)
                throw new EntityNotFoundException(nameof(InfoPost), request.InfoPostId);
            if (updatingPost.AuthorId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            updatingPost.TextContent = request.TextContent;
            updatingPost.Attachments = request.Attachments;
            updatingPost.CommentsAllowed = request.CommentsAllowed;
            updatingPost.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<InfoPostViewModel>(updatingPost);
        }
    }
}
