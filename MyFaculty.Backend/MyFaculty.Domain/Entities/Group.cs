using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public List<Pair> Pairs { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
