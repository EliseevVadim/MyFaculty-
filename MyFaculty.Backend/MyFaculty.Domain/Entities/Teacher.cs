﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public string PhotoPath { get; set; }
        public Guid ScienceRankId { get; set; }
        public ScienceRank ScienceRank { get; set; }
        public List<Auditorium> Auditoriums { get; set; } = new();
        public List<Pair> Pairs { get; set; } = new();
        public List<TeacherDiscipline> TeacherDisciplines { get; set; } = new();
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
