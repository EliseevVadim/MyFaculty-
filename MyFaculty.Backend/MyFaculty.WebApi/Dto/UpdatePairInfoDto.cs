using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo;

namespace MyFaculty.WebApi.Dto
{
    public class UpdatePairInfoDto : IMapWith<UpdatePairInfoCommand>
    {
        public int Id { get; set; }
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePairInfoDto, UpdatePairInfoCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.PairNumber, options => options.MapFrom(dto => dto.PairNumber))
                .ForMember(command => command.StartTime, options => options.MapFrom(dto => dto.StartTime))
                .ForMember(command => command.EndTime, options => options.MapFrom(dto => dto.EndTime));
        }
    }
}
