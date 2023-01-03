using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Commands.VerifyTeacher
{
    public class VerifyTeacherCommandHandler : IRequestHandler<VerifyTeacherCommand, TeacherVerificationDto>
    {
        private IMFDbContext _context;

        public VerifyTeacherCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<TeacherVerificationDto> Handle(VerifyTeacherCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);
            Teacher teacher = await _context.Teachers.FirstOrDefaultAsync(teacher => teacher.VerifiactionToken == request.VerificationToken, cancellationToken);
            bool verificationIsSuccessful = user != null 
                && teacher != null 
                && teacher.Email == user.Email 
                && teacher.VerifiactionToken == request.VerificationToken;
            return new TeacherVerificationDto
            {
                VerificationIsSuccessful = verificationIsSuccessful,
                VerifyingAcconut = user
            };
        }
    }
}
