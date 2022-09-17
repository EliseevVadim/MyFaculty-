using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Faculties.Commands.CreateFaculty;

namespace MyFaculty.WebApi.Dto
{
    public class CreateFacultyDto : IMapWith<CreateFacultyCommand>
    {
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFacultyDto, CreateFacultyCommand>()
                .ForMember(command => command.FacultyName, options => options.MapFrom(dto => dto.FacultyName))
                .ForMember(command => command.OfficialWebsite, options => options.MapFrom(dto => dto.OfficialWebsite));
        }
    }
}
