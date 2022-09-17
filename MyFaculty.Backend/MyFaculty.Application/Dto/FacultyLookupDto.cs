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
    public class FacultyLookupDto : IMapWith<Faculty>
    {
        public int Id { get; set; }
        public string FacultyName { get; set; }
        public string OfficialWebsite { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Faculty, FacultyLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(faculty => faculty.Id))
                .ForMember(dto => dto.FacultyName, options => options.MapFrom(faculty => faculty.FacultyName))
                .ForMember(dto => dto.OfficialWebsite, options => options.MapFrom(faculty => faculty.OfficialWebsite));
        }
    }
}
