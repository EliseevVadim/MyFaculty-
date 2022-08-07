using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class SecondaryObjectType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }
        public List<SecondaryObject> SecondaryObjects { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
