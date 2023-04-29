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

namespace MyFaculty.Application.Features.SecondaryObjects.Commands.UpdateSecondaryObject
{
    public class UpdateSecondaryObjectCommandHandler : IRequestHandler<UpdateSecondaryObjectCommand, SecondaryObjectViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateSecondaryObjectCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectViewModel> Handle(UpdateSecondaryObjectCommand request, CancellationToken cancellationToken)
        {
            SecondaryObject secondaryObject = await _context.SecondaryObjects.FirstOrDefaultAsync(secondaryObject => secondaryObject.Id == request.Id, cancellationToken);
            if (secondaryObject == null)
                throw new EntityNotFoundException(nameof(SecondaryObject), request.Id);
            secondaryObject.FloorId = request.FloorId;
            secondaryObject.SecondaryObjectTypeId = request.SecondaryObjectTypeId;
            secondaryObject.PositionInfo = request.PositionInfo;
            secondaryObject.ObjectName = request.ObjectName;
            secondaryObject.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SecondaryObjectViewModel>(secondaryObject);
        }
    }
}
