using MediatR;
using MyFaculty.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Queries.GetVerificationTokenQuery
{
    public class GetVerificationTokenQuery : IRequest<TeacherVerificationCredentialsDto>
    {
        public int Id { get; set; }
    }
}
