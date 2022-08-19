using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Teachers.Commands.UpdateTeacher;
using System;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateTeacherDto : IMapWith<UpdateTeacherCommand>
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string ScienceRankId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTeacherDto, UpdateTeacherCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.FIO, options => options.MapFrom(dto => dto.FIO))
                .ForMember(command => command.PhotoPath, options => options.MapFrom(dto => dto.PhotoPath))
                .ForMember(command => command.Email, options => options.MapFrom(dto => dto.Email))
                .ForMember(command => command.BirthDate, options => options.MapFrom(dto => dto.BirthDate))
                .ForMember(command => command.ScienceRankId, options => options.MapFrom(dto => dto.ScienceRankId));
        }
    }
}
