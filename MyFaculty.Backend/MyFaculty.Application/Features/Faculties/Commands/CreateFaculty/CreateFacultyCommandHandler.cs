using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Faculties.Commands.CreateFaculty
{
    public class CreateFacultyCommandHandler : IRequestHandler<CreateFacultyCommand, FacultyViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public CreateFacultyCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FacultyViewModel> Handle(CreateFacultyCommand request, CancellationToken cancellationToken)
        {
            Faculty faculty = new Faculty
            {
                FacultyName = request.FacultyName,
                OfficialWebsite = request.OfficialWebsite,
                Created = DateTime.Now
            };
            await _context.Faculties.AddAsync(faculty);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<FacultyViewModel>(faculty);
        }
    }
}
