using MediatR;
using MyFaculty.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Commands.VerifyTeacher
{
    public class VerifyTeacherCommand : IRequest<TeacherVerificationDto>
    {
        public int UserId { get; set; }
        public Guid VerificationToken { get; set; }
    }
}
