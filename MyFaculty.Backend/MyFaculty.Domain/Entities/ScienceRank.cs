using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class ScienceRank
    {
        public Guid Id { get; set; }
        public string RankName { get; set; }
        public List<Teacher> Teachers { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
