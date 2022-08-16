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

namespace MyFaculty.Application.Features.Auditoriums.Queries.GetAuditoriumInfo
{
    public class GetAuditoriumInfoQueryHandler : IRequestHandler<GetAuditoriumInfoQuery, AuditoriumViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetAuditoriumInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuditoriumViewModel> Handle(GetAuditoriumInfoQuery request, CancellationToken cancellationToken)
        {
            Auditorium auditorium = await _context.Auditoriums.FirstOrDefaultAsync(auditorium => auditorium.Id == request.Id, cancellationToken);
            if (auditorium == null)
                throw new EntityNotFoundException(nameof(Auditorium), request.Id);
            return _mapper.Map<AuditoriumViewModel>(auditorium);
        }
    }
}
