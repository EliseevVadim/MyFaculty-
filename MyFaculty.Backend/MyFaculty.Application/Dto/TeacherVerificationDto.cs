using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class TeacherVerificationDto
    {
        public bool VerificationIsSuccessful { get; set; }
        public AppUser VerifyingAcconut { get; set; }
    }
}
