using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class ScienceRankViewModel : IMapWith<ScienceRank>
    {
        public int Id { get; set; }
        public string RankName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ScienceRank, ScienceRankViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(rank => rank.Id))
                .ForMember(vm => vm.RankName, options => options.MapFrom(rank => rank.RankName))
                .ForMember(vm => vm.Created, options => options.MapFrom(rank => rank.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(rank => rank.Updated));
        }
    }
}
