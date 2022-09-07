using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Teachers.Commands.CreateTeacher;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyFaculty.WebApi.Dto
{
    public class CreateTeacherDto : IMapWith<CreateTeacherCommand>
    {
        [Required]
        public string FIO { get; set; }
        public IFormFile Photo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public int ScienceRankId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTeacherDto, CreateTeacherCommand>()
                .ForMember(command => command.FIO, options => options.MapFrom(dto => dto.FIO))
                .ForMember(command => command.Email, options => options.MapFrom(dto => dto.Email))
                .ForMember(command => command.BirthDate, options => options.MapFrom(dto => dto.BirthDate))
                .ForMember(command => command.ScienceRankId, options => options.MapFrom(dto => dto.ScienceRankId));
        }
    }
}
