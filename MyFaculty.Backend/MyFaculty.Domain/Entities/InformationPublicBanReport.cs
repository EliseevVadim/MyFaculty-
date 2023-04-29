using MyFaculty.Domain.Enums;
using System;

namespace MyFaculty.Domain.Entities
{
    public class InformationPublicBanReport
    {
        public int Id { get; set; }
        public int PublicId { get; set; }
        public int AdministratorId { get; set; }
        public string Reason { get; set; }
        public BanAction PerformedAction { get; set; }
        public InformationPublic AffectedPublic { get; set; }
        public AppUser Administrator { get; set; }
        public DateTime Created { get; set; }
    }
}
