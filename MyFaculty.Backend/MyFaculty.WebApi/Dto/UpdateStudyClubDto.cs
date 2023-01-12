using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.StudyClubs.Commands.UpdateStudyClub;
using System.ComponentModel.DataAnnotations;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateStudyClubDto : IMapWith<UpdateStudyClubCommand>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string StudyClubName { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public int OwnerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateStudyClubDto, UpdateStudyClubCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.StudyClubName, options => options.MapFrom(dto => dto.StudyClubName))
                .ForMember(command => command.Description, options => options.MapFrom(dto => dto.Description))
                .ForMember(command => command.OwnerId, options => options.MapFrom(dto => dto.OwnerId));
        }
    }
}
