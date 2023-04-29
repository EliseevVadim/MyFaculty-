using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFaculty.Domain.Entities
{
    public class SecondaryObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string PositionInfo { get; set; }
        public int SecondaryObjectTypeId { get; set; }
        public SecondaryObjectType SecondaryObjectType { get; set; }
        public int FloorId { get; set; }
        public Floor Floor { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
