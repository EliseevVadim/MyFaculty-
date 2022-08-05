using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Auditorium
    {
        public Guid Id { get; set; }
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public Guid FloorId { get; set; }
        public Floor Floor { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public List<Pair> Pairs { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
