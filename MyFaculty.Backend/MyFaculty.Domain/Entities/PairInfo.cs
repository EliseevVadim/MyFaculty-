using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFaculty.Domain.Entities
{
    public class PairInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PairNumber { get; set; }
        public List<Pair> Pairs { get; set; } = new();
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
