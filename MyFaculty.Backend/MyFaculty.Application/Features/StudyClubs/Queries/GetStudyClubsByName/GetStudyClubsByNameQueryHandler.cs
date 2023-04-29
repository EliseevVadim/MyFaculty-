using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.StudyClubs.Queries.GetStudyClubsByName
{
    public class GetStudyClubsByNameQueryHandler : IRequestHandler<GetStudyClubsByNameQuery, StudyClubsListViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public GetStudyClubsByNameQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudyClubsListViewModel> Handle(GetStudyClubsByNameQuery request, CancellationToken cancellationToken)
        {
            var clubs = await _context.StudyClubs
                .Where(club => club.ClubName.Contains(request.SearchRequest))
                .ProjectTo<StudyClubLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new StudyClubsListViewModel
            {
                StudyClubs = clubs
            };
        }
    }
}
