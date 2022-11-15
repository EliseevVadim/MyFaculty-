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
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, UserLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(user => user.Id))
                .ForMember(dto => dto.Email, options => options.MapFrom(user => user.Email))
                .ForMember(dto => dto.FirstName, options => options.MapFrom(user => user.FirstName))
                .ForMember(dto => dto.LastName, options => options.MapFrom(user => user.LastName))
                .ForMember(dto => dto.AvatarPath, options => options.MapFrom(user => user.AvatarPath))
                .ForMember(dto => dto.CityId, options => options.MapFrom(user => user.CityId))
                .ForMember(dto => dto.CityName, options => options.MapFrom(user => user.City.CityName))
                .ForMember(dto => dto.CountryName, options => options.MapFrom(user => user.City.Region.Country.CountryName));
        }
    }
}
