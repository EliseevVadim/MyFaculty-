using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Application.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Queries.GetTeachers
{
    public class GetTeachersQueryHandler : IRequestHandler<GetTeachersQuery, TeachersListViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetTeachersQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeachersListViewModel> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _context.Teachers
                .ProjectTo<TeacherLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new TeachersListViewModel()
            {
                Teachers = teachers
            };
        }
    }
}
