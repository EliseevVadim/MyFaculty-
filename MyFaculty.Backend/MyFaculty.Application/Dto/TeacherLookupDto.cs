using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class TeacherLookupDto : IMapWith<Teacher>
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int ScienceRankId { get; set; }
        public string ScienceRankName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, TeacherLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(teacher => teacher.Id))
                .ForMember(dto => dto.FIO, options => options.MapFrom(teacher => teacher.FIO))
                .ForMember(dto => dto.PhotoPath, options => options.MapFrom(teacher => teacher.PhotoPath))
                .ForMember(dto => dto.Email, options => options.MapFrom(teacher => teacher.Email))
                .ForMember(dto => dto.BirthDate, options => options.MapFrom(teacher => teacher.BirthDate))
                .ForMember(dto => dto.ScienceRankId, options => options.MapFrom(teacher => teacher.ScienceRankId))
                .ForMember(dto => dto.ScienceRankName, options => options.MapFrom(teacher => teacher.ScienceRank.RankName));
        }
    }
}
