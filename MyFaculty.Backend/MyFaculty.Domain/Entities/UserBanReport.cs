using MyFaculty.Domain.Enums;
using System;

namespace MyFaculty.Domain.Entities
{
    public class UserBanReport
    {
        public int Id { get; set; }
        public int AffectedUserId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
        public BanAction PerformedAction { get; set; }
        public AppUser AffectedUser { get; set; }
        public AppUser Administrator { get; set; }
        public DateTime Created { get; set; }
    }
}
