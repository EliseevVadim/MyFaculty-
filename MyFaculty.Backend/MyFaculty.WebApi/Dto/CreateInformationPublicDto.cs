using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InformationPublics.Commands.CreateInformationPublic;
using System.ComponentModel.DataAnnotations;

namespace MyFaculty.WebApi.Dto
{
    public class CreateInformationPublicDto : IMapWith<CreateInformationPublicCommand>
    {
        [Required]
        public string PublicName { get; set; }
        [Required]
        public int IssuerId { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public int OwnerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateInformationPublicDto, CreateInformationPublicCommand>()
                .ForMember(command => command.PublicName, options => options.MapFrom(dto => dto.PublicName))
                .ForMember(command => command.Description, options => options.MapFrom(dto => dto.Description))
                .ForMember(command => command.OwnerId, options => options.MapFrom(dto => dto.OwnerId));
        }
    }
}
