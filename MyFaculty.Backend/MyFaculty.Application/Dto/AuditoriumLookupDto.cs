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
    public class AuditoriumLookupDto : IMapWith<Auditorium>
    {
        public int Id { get; set; }
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Auditorium, AuditoriumLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(auditorium => auditorium.Id))
                .ForMember(dto => dto.AuditoriumName, options => options.MapFrom(auditorium => auditorium.AuditoriumName))
                .ForMember(dto => dto.PositionInfo, options => options.MapFrom(auditorium => auditorium.PositionInfo))
                .ForMember(dto => dto.FloorId, options => options.MapFrom(auditorium => auditorium.FloorId))
                .ForMember(dto => dto.FloorName, options => options.MapFrom(auditorium => auditorium.Floor.Name))
                .ForMember(dto => dto.TeacherId, options => options.MapFrom(auditorium => auditorium.TeacherId))
                .ForMember(dto => dto.TeachersFIO, options => options.MapFrom(auditorium => auditorium.Teacher.FIO));
        }
    }
}
