using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Queries.GetSecondaryObjectTypeInfo
{
    public class GetSecondaryObjectTypeInfoQueryHandler : IRequestHandler<GetSecondaryObjectTypeInfoQuery, SecondaryObjectTypeViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetSecondaryObjectTypeInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectTypeViewModel> Handle(GetSecondaryObjectTypeInfoQuery request, CancellationToken cancellationToken)
        {
            SecondaryObjectType type = await _context.SecondaryObjectTypes.FirstOrDefaultAsync(type => type.Id == request.Id, cancellationToken);
            if (type == null)
                throw new EntityNotFoundException(nameof(SecondaryObjectType), request.Id);
            return _mapper.Map<SecondaryObjectTypeViewModel>(type);
        }
    }
}
