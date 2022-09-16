using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Floor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bounds { get; set; }
        public int? FacultyId { get; set; }
        public Faculty Faculty { get; set; }
        public List<Auditorium> Auditoriums { get; set; } = new();
        public List<SecondaryObject> SecondaryObjects { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
