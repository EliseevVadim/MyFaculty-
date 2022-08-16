﻿using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class DisciplineLookupDto : IMapWith<Discipline>
    {
        public int Id { get; set; }
        public string DisciplineName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Discipline, DisciplineLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(discipline => discipline.Id))
                .ForMember(dto => dto.DisciplineName, options => options.MapFrom(discipline => discipline.DisciplineName));
        }
    }
}
