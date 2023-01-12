using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubs
{
    public class GetStudyClubsQueryHandler : IRequestHandler<GetStudyClubsQuery, StudyClubsListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetStudyClubsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudyClubsListViewModel> Handle(GetStudyClubsQuery request, CancellationToken cancellationToken)
        {
            var clubs = await _context.StudyClubs
                .ProjectTo<StudyClubLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new StudyClubsListViewModel
            {
                StudyClubs = clubs
            };
        }
    }
}
