using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Commands.UnlikeInfoPost
{
    public class UnlikeInfoPostCommandHandler : IRequestHandler<UnlikeInfoPostCommand>
    {
        private readonly IMFDbContext _context;

        public UnlikeInfoPostCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UnlikeInfoPostCommand request, CancellationToken cancellationToken)
        {
            InfoPost infoPost = await _context.InfoPosts
                .Include(post => post.OwningInformationPublic)
                    .ThenInclude(infoPublic => infoPublic.BlockedUsers)
                .Include(post => post.LikedUsers)
                .FirstOrDefaultAsync(post => post.Id == request.LikedPostId, cancellationToken);
            if (infoPost == null || (infoPost.OwningInformationPublic != null && infoPost.OwningInformationPublic.IsBanned))
                throw new EntityNotFoundException(nameof(InfoPost), request.LikedPostId);
            AppUser user = await _context.Users.FindAsync(new object[] { request.LikedUserId }, cancellationToken);
            if (user == null)
                throw new EntityNotFoundException(nameof(AppUser), request.LikedUserId);
            if (infoPost.OwningInformationPublic != null)
                if (infoPost.OwningInformationPublic.BlockedUsers.Contains(user))
                    throw new DestructiveActionException("Вы не можете взаимодействовать с этой записью, так как были заблокированы " +
                        "в сообществе, содержащем ее");
            infoPost.LikedUsers.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
