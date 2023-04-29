using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class TeacherVerificationDto
    {
        public bool VerificationIsSuccessful { get; set; }
        public AppUser VerifyingAcconut { get; set; }
    }
}
