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

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPost
{
    public class GetInfoPostQueryHandler : IRequestHandler<GetInfoPostQuery, InfoPostViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetInfoPostQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostViewModel> Handle(GetInfoPostQuery request, CancellationToken cancellationToken)
        {
            InfoPost infoPost = await _context.InfoPosts
                .Include(post => post.OwningInformationPublic)
                    .ThenInclude(infoPublic => infoPublic.BlockedUsers)
                .Include(post => post.OwningStudyClub)
                .Include(post => post.LikedUsers)
                .Include(post => post.Author)
                .FirstOrDefaultAsync(infoPost => infoPost.Id == request.Id, cancellationToken);
            if (infoPost == null || (infoPost.OwningInformationPublic != null && infoPost.OwningInformationPublic.IsBanned))
                throw new EntityNotFoundException(nameof(InfoPost), request.Id);
            if (infoPost.OwningInformationPublic != null)
                if (infoPost.OwningInformationPublic.BlockedUsers.Select(user => user.Id).Contains(request.IssuerId))
                    throw new DestructiveActionException("Вы не можете просмотреть эту запись, так как были заблокированы " +
                        "в сообществе, содержащем ее");
            return _mapper.Map<InfoPostViewModel>(infoPost);
        }
    }
}
