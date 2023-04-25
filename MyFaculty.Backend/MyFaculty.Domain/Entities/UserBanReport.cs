using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class UserBanReport
    {
        public int Id { get; set; }
        public int AffectedUserId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
        public UserBanAction PerformedAction { get; set; }
        public AppUser AffectedUser { get; set; }
        public AppUser Administrator { get; set; }
        public DateTime Created { get; set; }
    }
}
