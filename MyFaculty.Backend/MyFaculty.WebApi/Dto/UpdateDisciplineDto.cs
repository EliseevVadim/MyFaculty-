using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Disciplines.Commands.UpdateDiscipline;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateDisciplineDto : IMapWith<UpdateDisciplineCommand>
    {
        public int Id { get; set; }
        public string DisciplineName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateDisciplineDto, UpdateDisciplineCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.DisciplineName, options => options.MapFrom(dto => dto.DisciplineName));
        }
    }
}
