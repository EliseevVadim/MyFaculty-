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

namespace MyFaculty.Application.Features.InformationPublics.Commands.CreateInformationPublic
{
    public class CreateInformationPublicCommandHandler : IRequestHandler<CreateInformationPublicCommand, InformationPublicViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public CreateInformationPublicCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InformationPublicViewModel> Handle(CreateInformationPublicCommand request, CancellationToken cancellationToken)
        {
            AppUser owner = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.OwnerId);
            if (owner == null)
                throw new EntityNotFoundException(nameof(AppUser), request.OwnerId);
            InformationPublic infoPublic = new InformationPublic
            {
                PublicName = request.PublicName,
                Description = request.Description,
                ImagePath = request.ImagePath,
                OwnerId = request.OwnerId,
                Created = DateTime.Now
            };
            infoPublic.Members.Add(owner);
            await _context.InformationPublics.AddAsync(infoPublic, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<InformationPublicViewModel>(infoPublic);
        }
    }
}
