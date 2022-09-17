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

namespace MyFaculty.Application.Features.Faculties.Commands.UpdateFaculty
{
    public class UpdateFacultyCommandHandler : IRequestHandler<UpdateFacultyCommand, FacultyViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public UpdateFacultyCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FacultyViewModel> Handle(UpdateFacultyCommand request, CancellationToken cancellationToken)
        {
            Faculty faculty = await _context.Faculties.FirstOrDefaultAsync(faculty => faculty.Id == request.Id, cancellationToken);
            if (faculty == null)
                throw new EntityNotFoundException(nameof(Faculty), request.Id);
            faculty.FacultyName = request.FacultyName;
            faculty.OfficialWebsite = request.OfficialWebsite;
            faculty.Updated = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<FacultyViewModel>(faculty);
        }
    }
}
