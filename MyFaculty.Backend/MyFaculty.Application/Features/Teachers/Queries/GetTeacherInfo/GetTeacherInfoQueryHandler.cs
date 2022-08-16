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

namespace MyFaculty.Application.Features.Teachers.Queries.GetTeacherInfo
{
    public class GetTeacherInfoQueryHandler : IRequestHandler<GetTeacherInfoQuery, TeacherViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetTeacherInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherViewModel> Handle(GetTeacherInfoQuery request, CancellationToken cancellationToken)
        {
            Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.Id == request.Id, cancellationToken);
            if (teacher == null)
                throw new EntityNotFoundException(nameof(Teacher), request.Id);
            return _mapper.Map<TeacherViewModel>(teacher);
        }
    }
}
