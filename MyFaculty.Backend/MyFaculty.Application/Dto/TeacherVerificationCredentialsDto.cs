using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.Dto
{
    public class TeacherVerificationCredentialsDto : IMapWith<Teacher>
    {
        public string Email { get; set; }
        public Guid VerificationToken { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, TeacherVerificationCredentialsDto>()
                .ForMember(dto => dto.Email, options => options.MapFrom(teacher => teacher.Email))
                .ForMember(dto => dto.VerificationToken, options => options.MapFrom(teacher => teacher.VerifiactionToken));
        }
    }
}
