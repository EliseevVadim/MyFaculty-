using System;
using System.Collections.Generic;

namespace MyFaculty.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public List<Region> Regions { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
