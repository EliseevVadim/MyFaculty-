using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Faculties.Commands.UpdateFaculty;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateFacultyDto : IMapWith<UpdateFacultyCommand>
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFacultyDto, UpdateFacultyCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.FacultyName, options => options.MapFrom(dto => dto.FacultyName))
                .ForMember(command => command.OfficialWebsite, options => options.MapFrom(dto => dto.OfficialWebsite));
        }
    }
}
