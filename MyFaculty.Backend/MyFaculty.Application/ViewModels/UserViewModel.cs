using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class UserViewModel : IMapWith<AppUser>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public CityLookupDto City { get; set; }
        public string Website { get; set; }
        public string VKLink { get; set; }
        public string TelegramLink { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AppUser, UserViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(user => user.Id))
                .ForMember(vm => vm.Email, options => options.MapFrom(user => user.Email))
                .ForMember(vm => vm.FirstName, options => options.MapFrom(user => user.FirstName))
                .ForMember(vm => vm.LastName, options => options.MapFrom(user => user.LastName))
                .ForMember(vm => vm.AvatarPath, options => options.MapFrom(user => user.AvatarPath))
                .ForMember(vm => vm.BirthDate, options => options.MapFrom(user => user.BirthDate))
                .ForMember(vm => vm.CityId, options => options.MapFrom(user => user.CityId))
                .ForMember(vm => vm.City, options => options.MapFrom(user => user.City))
                .ForMember(vm => vm.Website, options => options.MapFrom(user => user.Website))
                .ForMember(vm => vm.VKLink, options => options.MapFrom(user => user.VKLink))
                .ForMember(vm => vm.TelegramLink, options => options.MapFrom(user => user.TelegramLink));
        }
    }
}
