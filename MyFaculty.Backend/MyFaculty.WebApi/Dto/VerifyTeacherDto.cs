using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Teachers.Commands.VerifyTeacher;
using System;

namespace MyFaculty.WebApi.Dto
{
    public class VerifyTeacherDto : IMapWith<VerifyTeacherCommand>
    {
        public int UserId { get; set; }
        public Guid VerificationToken { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VerifyTeacherDto, VerifyTeacherCommand>()
                .ForMember(command => command.UserId, options => options.MapFrom(dto => dto.UserId))
                .ForMember(command => command.VerificationToken, options => options.MapFrom(dto => dto.VerificationToken));
        }
    }
}
