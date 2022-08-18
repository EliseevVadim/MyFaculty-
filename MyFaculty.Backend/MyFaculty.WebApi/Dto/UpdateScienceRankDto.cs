using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.ScienceRanks.Commands.UpdateScienceRank;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateScienceRankDto : IMapWith<UpdateScienceRankCommand>
    {
        public int Id { get; set; }
        public string RankName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateScienceRankDto, UpdateScienceRankCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.RankName, options => options.MapFrom(dto => dto.RankName));
        }
    }
}
