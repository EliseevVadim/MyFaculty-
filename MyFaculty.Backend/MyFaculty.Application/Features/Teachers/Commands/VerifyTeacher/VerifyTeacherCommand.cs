using MediatR;
using MyFaculty.Application.Dto;
using System;

namespace MyFaculty.Application.Features.Teachers.Commands.VerifyTeacher
{
    public class VerifyTeacherCommand : IRequest<TeacherVerificationDto>
    {
        public int UserId { get; set; }
        public Guid VerificationToken { get; set; }
    }
}
