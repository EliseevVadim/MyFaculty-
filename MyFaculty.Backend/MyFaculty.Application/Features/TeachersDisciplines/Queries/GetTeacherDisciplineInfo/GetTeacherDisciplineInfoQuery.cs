﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.TeachersDisciplines.Queries.GetTeacherDisciplineInfo
{
    public class GetTeacherDisciplineInfoQuery : IRequest<TeacherDisciplineViewModel>
    {
        public int Id { get; set; }
    }
}
