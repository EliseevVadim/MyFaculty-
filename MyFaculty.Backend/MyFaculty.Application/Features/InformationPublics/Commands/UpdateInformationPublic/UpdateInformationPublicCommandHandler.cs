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

namespace MyFaculty.Application.Features.InformationPublics.Commands.UpdateInformationPublic
{
    public class UpdateInformationPublicCommandHandler : IRequestHandler<UpdateInformationPublicCommand, InformationPublicViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateInformationPublicCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InformationPublicViewModel> Handle(UpdateInformationPublicCommand request, CancellationToken cancellationToken)
        {
            InformationPublic infoPublic = await _context.InformationPublics.FirstOrDefaultAsync(infoPublic => infoPublic.Id == request.Id, cancellationToken);
            if (infoPublic == null || infoPublic.IsBanned)
                throw new EntityNotFoundException(nameof(InformationPublic), request.Id);
            AppUser owner = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.OwnerId);
            if (owner == null)
                throw new EntityNotFoundException(nameof(AppUser), request.OwnerId);
            if (infoPublic.OwnerId != request.IssuerId)
                throw new UnauthorizedActionException("Данное действие Вам запрещено.");
            infoPublic.PublicName = request.PublicName;
            infoPublic.Description = request.Description;
            infoPublic.ImagePath = String.IsNullOrEmpty(request.ImagePath) ? infoPublic.ImagePath : request.ImagePath;
            infoPublic.OwnerId = request.OwnerId;
            infoPublic.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<InformationPublicViewModel>(infoPublic);
        }
    }
}
