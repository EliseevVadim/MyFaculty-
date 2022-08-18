using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType
{
    public class CreateSecondaryObjectTypeCommandHandler : IRequestHandler<CreateSecondaryObjectTypeCommand, SecondaryObjectTypeViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateSecondaryObjectTypeCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectTypeViewModel> Handle(CreateSecondaryObjectTypeCommand request, CancellationToken cancellationToken)
        {
            SecondaryObjectType type = new SecondaryObjectType()
            {
                ObjectTypeName = request.ObjectTypeName,
                TypePath = request.TypePath,
                Created = DateTime.Now
            };
            await _context.SecondaryObjectTypes.AddAsync(type, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SecondaryObjectTypeViewModel>(type);
        }
    }
}
