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

namespace MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjectInfo
{
    public class GetSecondaryObjectInfoQueryHandler : IRequestHandler<GetSecondaryObjectInfoQuery, SecondaryObjectViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetSecondaryObjectInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SecondaryObjectViewModel> Handle(GetSecondaryObjectInfoQuery request, CancellationToken cancellationToken)
        {
            SecondaryObject secondaryObject = await _context.SecondaryObjects
                .Include(secondaryObject => secondaryObject.Floor)
                    .ThenInclude(floor => floor.Faculty)
                .FirstOrDefaultAsync(secondaryObject => secondaryObject.Id == request.Id, cancellationToken);
            if (secondaryObject == null)
                throw new EntityNotFoundException(nameof(SecondaryObject), request.Id);
            return _mapper.Map<SecondaryObjectViewModel>(secondaryObject);
        }
    }
}
