﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class DayOfWeek
    {
        public Guid Id { get; set; }
        public string DaysName { get; set; }
        public List<Pair> Pairs { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
