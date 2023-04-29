using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Dto
{
    public class PairRepeatingLookupDto : IMapWith<PairRepeating>
    {
        public int Id { get; set; }
        public string RepeatingName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PairRepeating, PairRepeatingLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(repeating => repeating.Id))
                .ForMember(dto => dto.RepeatingName, options => options.MapFrom(repeating => repeating.RepeatingName));
        }
    }
}
