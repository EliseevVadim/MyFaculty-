using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.ScienceRanks.Commands.CreateScienceRank;

namespace MyFaculty.WebApi.Dto
{
    public class CreateScienceRankDto : IMapWith<CreateScienceRankCommand>
    {
        public string RankName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateScienceRankDto, CreateScienceRankCommand>()
                .ForMember(command => command.RankName, options => options.MapFrom(dto => dto.RankName));
        }
    }
}
