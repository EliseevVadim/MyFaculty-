using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Queries.GetInformationPublicInfo
{
    public class GetInformationPublicInfoQueryHandler : IRequestHandler<GetInformationPublicInfoQuery, InformationPublicViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetInformationPublicInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InformationPublicViewModel> Handle(GetInformationPublicInfoQuery request, CancellationToken cancellationToken)
        {
            InformationPublic infoPublic = await _context.InformationPublics
                .Include(infoPublic => infoPublic.Members)
                .Include(infoPublic => infoPublic.Moderators)
                .Include(infoPublic => infoPublic.BlockedUsers)
                .FirstOrDefaultAsync(infoPublic => infoPublic.Id == request.Id, cancellationToken);
            if (infoPublic == null)
                throw new EntityNotFoundException(nameof(InformationPublic), request.Id);
            return _mapper.Map<InformationPublicViewModel>(infoPublic);
        }
    }
}
