using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Disciplines.Commands.CreateDiscipline;

namespace MyFaculty.WebApi.Dto
{
    public class CreateDisciplineDto : IMapWith<CreateDisciplineCommand>
    {
        public string DisciplineName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateDisciplineDto, CreateDisciplineCommand>()
                .ForMember(command => command.DisciplineName, options => options.MapFrom(dto => dto.DisciplineName));
        }
    }
}
