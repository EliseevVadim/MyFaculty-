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
    public class AuditoriumViewModel : IMapWith<Auditorium>
    {
        public int Id { get; set; }
        public string AuditoriumName { get; set; }
        public string PositionInfo { get; set; }
        public FloorLookupDto Floor { get; set; }
        public int TeacherId { get; set; }
        public string TeachersFIO { get; set; }
        public List<PairLookupDto> Pairs { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Auditorium, AuditoriumViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(auditorium => auditorium.Id))
                .ForMember(vm => vm.AuditoriumName, options => options.MapFrom(auditorium => auditorium.AuditoriumName))
                .ForMember(vm => vm.PositionInfo, options => options.MapFrom(auditorium => auditorium.PositionInfo))
                .ForMember(vm => vm.Floor, options => options.MapFrom(auditorium => auditorium.Floor))
                .ForMember(vm => vm.TeacherId, options => options.MapFrom(auditorium => auditorium.TeacherId))
                .ForMember(vm => vm.TeachersFIO, options => options.MapFrom(auditorium => auditorium.Teacher.FIO))
                .ForMember(vm => vm.Pairs, options => options.MapFrom(auditorium => auditorium.Pairs))
                .ForMember(vm => vm.Created, options => options.MapFrom(auditorium => auditorium.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(auditorium => auditorium.Updated));
        }
    }
}
