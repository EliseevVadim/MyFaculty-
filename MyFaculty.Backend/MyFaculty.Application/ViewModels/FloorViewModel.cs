using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class FloorViewModel : IMapWith<Floor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bounds { get; set; }
        public List<AuditoriumLookupDto> Auditoriums { get; set; }
        public List<SecondaryObjectLookupDto> SecondaryObjects { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Floor, FloorViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(floor => floor.Id))
                .ForMember(vm => vm.Name, options => options.MapFrom(floor => floor.Name))
                .ForMember(vm => vm.Bounds, options => options.MapFrom(floor => floor.Bounds))
                .ForMember(vm => vm.Auditoriums, options => options.MapFrom(floor => floor.Auditoriums))
                .ForMember(vm => vm.SecondaryObjects, options => options.MapFrom(floor => floor.SecondaryObjects))
                .ForMember(vm => vm.Created, options => options.MapFrom(floor => floor.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(floor => floor.Updated));
        }
    }
}
