using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class PairInfoViewModel : IMapWith<PairInfo>
    {
        public int Id { get; set; }
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PairInfo, PairInfoViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(info => info.Id))
                .ForMember(vm => vm.PairNumber, options => options.MapFrom(info => info.PairNumber))
                .ForMember(vm => vm.StartTime, options => options.MapFrom(info => info.StartTime))
                .ForMember(vm => vm.EndTime, options => options.MapFrom(info => info.EndTime))
                .ForMember(vm => vm.Created, options => options.MapFrom(info => info.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(info => info.Updated));
        }
    }
}
