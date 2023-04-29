using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Queries.GetFacultyInfo
{
    public class GetFacultyInfoQueryHandler : IRequestHandler<GetFacultyInfoQuery, FacultyViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetFacultyInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FacultyViewModel> Handle(GetFacultyInfoQuery request, CancellationToken cancellationToken)
        {
            Faculty faculty = await _context.Faculties
                .Include(faculty => faculty.Floors)
                .FirstOrDefaultAsync(faculty => faculty.Id == request.Id);
            if (faculty == null)
                throw new EntityNotFoundException(nameof(Faculty), request.Id);
            return _mapper.Map<FacultyViewModel>(faculty);
        }
    }
}
