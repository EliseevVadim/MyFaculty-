using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Floor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Bounds { get; set; }
        public List<Auditorium> Auditoriums { get; set; } = new();
        public List<SecondaryObject> SecondaryObjects { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
