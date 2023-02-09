using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Queries.GetClubTaskInfo
{
    public class GetClubTaskInfoQueryHandler : IRequestHandler<GetClubTaskInfoQuery, ClubTaskViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetClubTaskInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClubTaskViewModel> Handle(GetClubTaskInfoQuery request, CancellationToken cancellationToken)
        {
            ClubTask task = await _context.ClubTasks.FirstOrDefaultAsync(task => task.Id == request.Id, cancellationToken);
            if (task == null)
                throw new EntityNotFoundException(nameof(ClubTask), request.Id);
            return _mapper.Map<ClubTaskViewModel>(task);
        }
    }
}
