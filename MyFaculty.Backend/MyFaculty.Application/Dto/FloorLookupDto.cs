using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class FloorLookupDto : IMapWith<Floor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bounds { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Floor, FloorLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(floor => floor.Id))
                .ForMember(dto => dto.Name, options => options.MapFrom(floor => floor.Name))
                .ForMember(dto => dto.Bounds, options => options.MapFrom(floor => floor.Bounds))
                .ForMember(dto => dto.FacultyId, options => options.MapFrom(floor => floor.FacultyId))
                .ForMember(dto => dto.FacultyName, options => options.MapFrom(floor => floor.Faculty.FacultyName));
        }
    }
}
