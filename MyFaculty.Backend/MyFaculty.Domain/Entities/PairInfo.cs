using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class PairInfo
    {
        public Guid Id { get; set; }
        public int PairNumber { get; set; }
        public List<Pair> Pairs { get; set; } = new();
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
