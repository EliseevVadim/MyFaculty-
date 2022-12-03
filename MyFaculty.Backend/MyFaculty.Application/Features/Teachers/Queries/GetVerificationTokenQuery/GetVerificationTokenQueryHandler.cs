using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Queries.GetVerificationTokenQuery
{
    public class GetVerificationTokenQueryHandler : IRequestHandler<GetVerificationTokenQuery, TeacherVerificationCredentialsDto>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetVerificationTokenQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TeacherVerificationCredentialsDto> Handle(GetVerificationTokenQuery request, CancellationToken cancellationToken)
        {
            Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.Id == request.Id, cancellationToken);
            if (teacher == null)
                throw new EntityNotFoundException(nameof(Teacher), request.Id);
            return _mapper.Map<TeacherVerificationCredentialsDto>(teacher);
        }
    }
}
