using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo;

namespace MyFaculty.WebApi.Dto
{
    public class CreatePairInfoDto : IMapWith<CreatePairInfoCommand>
    {
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePairInfoDto, CreatePairInfoCommand>()
                .ForMember(command => command.PairNumber, options => options.MapFrom(dto => dto.PairNumber))
                .ForMember(command => command.StartTime, options => options.MapFrom(dto => dto.StartTime))
                .ForMember(command => command.EndTime, options => options.MapFrom(dto => dto.EndTime));
        }
    }
}
