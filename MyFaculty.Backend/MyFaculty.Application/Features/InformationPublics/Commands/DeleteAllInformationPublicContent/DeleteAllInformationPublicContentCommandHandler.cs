using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteAllInformationPublicContent
{
    public class DeleteAllInformationPublicContentCommandHandler : IRequestHandler<DeleteAllInformationPublicContentCommand, List<string>>
    {
        private readonly IMFDbContext _context;

        public DeleteAllInformationPublicContentCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(DeleteAllInformationPublicContentCommand request, CancellationToken cancellationToken)
        {
            InformationPublic clearingPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.InfoPosts)
                    .ThenInclude(infoPost => infoPost.Comments)
                .FirstOrDefaultAsync(infoPublic => infoPublic.Id == request.PublicId, cancellationToken);
            if (clearingPublic == null)
                throw new EntityNotFoundException(nameof(InformationPublic), request.PublicId);
            if (!clearingPublic.IsBanned)
                throw new DestructiveActionException("Нельзя удалять контент незаблокированного сообщества");
            List<string> postsAttachments = clearingPublic.InfoPosts
                .Select(infoPost => infoPost.Attachments)
                .ToList();
            List<string> commentsAttachments = clearingPublic.InfoPosts
                .SelectMany(infoPost => infoPost.Comments.Select(comment => comment.Attachments))
                .ToList();
            postsAttachments.AddRange(commentsAttachments);
            _context.InfoPosts.RemoveRange(clearingPublic.InfoPosts);
            await _context.SaveChangesAsync(cancellationToken);
            return postsAttachments;
        }
    }
}
