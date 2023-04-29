using MediatR;
using MyFaculty.Application.Dto;

namespace MyFaculty.Application.Features.Teachers.Queries.GetVerificationTokenQuery
{
    public class GetVerificationTokenQuery : IRequest<TeacherVerificationCredentialsDto>
    {
        public int Id { get; set; }
    }
}
