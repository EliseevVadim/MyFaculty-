using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.UpdateInformationPublic;
using System.ComponentModel.DataAnnotations;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateInformationPublicDto : IMapWith<UpdateInformationPublicCommand>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int IssuerId { get; set; }
        [Required]
        public string PublicName { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public int OwnerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateInformationPublicDto, UpdateInformationPublicCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.PublicName, options => options.MapFrom(dto => dto.PublicName))
                .ForMember(command => command.Description, options => options.MapFrom(dto => dto.Description))
                .ForMember(command => command.OwnerId, options => options.MapFrom(dto => dto.OwnerId));
        }
    }
}
