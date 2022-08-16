using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType
{
    public class CreateSecondaryObjectTypeCommandHandler : IRequestHandler<CreateSecondaryObjectTypeCommand, SecondaryObjectType>
    {
        private IMFDbContext _context;

        public CreateSecondaryObjectTypeCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<SecondaryObjectType> Handle(CreateSecondaryObjectTypeCommand request, CancellationToken cancellationToken)
        {
            SecondaryObjectType type = new SecondaryObjectType()
            {
                ObjectTypeName = request.ObjectTypeName,
                TypePath = request.TypePath,
                Created = DateTime.Now
            };
            await _context.SecondaryObjectTypes.AddAsync(type, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return type;
        }
    }
}
