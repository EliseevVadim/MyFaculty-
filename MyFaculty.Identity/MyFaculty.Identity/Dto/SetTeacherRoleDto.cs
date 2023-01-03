using System;

namespace MyFaculty.Identity.Dto
{
    public class SetTeacherRoleDto
    {
        public int UserId { get; set; }
        public Guid VerificationToken { get; set; }
    }
}
