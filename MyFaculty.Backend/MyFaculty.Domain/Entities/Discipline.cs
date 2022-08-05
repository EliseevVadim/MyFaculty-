﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Discipline
    {
        public Guid Id { get; set; }
        public string DisciplineName { get; set; }
        public List<Pair> Pairs { get; set; } = new();
        public List<TeacherDiscipline> TeacherDisciplines { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
