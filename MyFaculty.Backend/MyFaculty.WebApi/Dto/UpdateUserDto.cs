using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Users.Commands.UpdateUser;
using System.ComponentModel.DataAnnotations;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public IFormFile Photo { get; set; }
        public int CityId { get; set; }
        public string Website { get; set; }
        public string VKLink { get; set; }
        public string TelegramLink { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.Email, options => options.MapFrom(dto => dto.Email))
                .ForMember(command => command.FirstName, options => options.MapFrom(dto => dto.FirstName))
                .ForMember(command => command.LastName, options => options.MapFrom(dto => dto.LastName))
                .ForMember(command => command.CityId, options => options.MapFrom(dto => dto.CityId))
                .ForMember(command => command.Website, options => options.MapFrom(dto => dto.Website))
                .ForMember(command => command.VKLink, options => options.MapFrom(dto => dto.VKLink))
                .ForMember(command => command.TelegramLink, options => options.MapFrom(dto => dto.TelegramLink));
        }
    }
}
