using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class SecondaryObjectViewModel : IMapWith<SecondaryObject>
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string PositionInfo { get; set; }
        public int SecondaryObjectTypeId { get; set; }
        public int FloorId { get; set; }
        public SecondaryObjectTypeViewModel SecondaryObjectType { get; set; }
        public string FloorName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SecondaryObject, SecondaryObjectViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(secondaryObject => secondaryObject.Id))
                .ForMember(vm => vm.ObjectName, options => options.MapFrom(secondaryObject => secondaryObject.ObjectName))
                .ForMember(vm => vm.PositionInfo, options => options.MapFrom(secondaryObject => secondaryObject.PositionInfo))
                .ForMember(vm => vm.SecondaryObjectTypeId, options => options.MapFrom(secondaryObject => secondaryObject.SecondaryObjectTypeId))
                .ForMember(vm => vm.FloorId, options => options.MapFrom(secondaryObject => secondaryObject.FloorId))
                .ForMember(vm => vm.SecondaryObjectType, options => options.MapFrom(secondaryObject => secondaryObject.SecondaryObjectType))
                .ForMember(vm => vm.FloorName, options => options.MapFrom(secondaryObject => secondaryObject.Floor.Name))
                .ForMember(vm => vm.Created, options => options.MapFrom(secondaryObject => secondaryObject.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(secondaryObject => secondaryObject.Updated));
        }
    }
}
