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
    public class UserLookupDto : IMapWith<AppUser>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public int? CityId { get; set; }
        public int? FacultyId { get; set; }
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }
        public bool IsTeacher { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string FacultyName { get; set; }
        public string CourseName { get; set; }
        public string GroupName { get; set; }
        public bool IsBanned { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, UserLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(user => user.Id))
                .ForMember(dto => dto.Email, options => options.MapFrom(user => user.Email))
                .ForMember(dto => dto.FirstName, options => options.MapFrom(user => user.FirstName))
                .ForMember(dto => dto.LastName, options => options.MapFrom(user => user.LastName))
                .ForMember(dto => dto.AvatarPath, options => options.MapFrom(user => user.AvatarPath))
                .ForMember(dto => dto.CityId, options => options.MapFrom(user => user.CityId))
                .ForMember(vm => vm.FacultyId, options => options.MapFrom(user => user.FacultyId))
                .ForMember(vm => vm.CourseId, options => options.MapFrom(user => user.CourseId))
                .ForMember(vm => vm.GroupId, options => options.MapFrom(user => user.GroupId))
                .ForMember(vm => vm.IsTeacher, options => options.MapFrom(user => user.IsTeacher))
                .ForMember(dto => dto.CityName, options => options.MapFrom(user => user.City.CityName))
                .ForMember(dto => dto.CountryName, options => options.MapFrom(user => user.City.Region.Country.CountryName))
                .ForMember(dto => dto.FacultyName, options => options.MapFrom(user => user.Faculty.FacultyName))
                .ForMember(dto => dto.CourseName, options => options.MapFrom(user => user.Course.CourseName))
                .ForMember(dto => dto.GroupName, options => options.MapFrom(user => user.Group.GroupName))
                .ForMember(dto => dto.IsBanned, options => options.MapFrom(user => user.IsBanned));
        }
    }
}
