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
    public class DisciplineViewModel : IMapWith<Discipline>
    {
        public int Id { get; set; }
        public string DisciplineName { get; set; }
        public List<PairLookupDto> Pairs { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Discipline, DisciplineViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(discipline => discipline.Id))
                .ForMember(vm => vm.DisciplineName, options => options.MapFrom(discipline => discipline.DisciplineName))
                .ForMember(vm => vm.Pairs, options => options.MapFrom(discipline => discipline.Pairs))
                .ForMember(vm => vm.Created, options => options.MapFrom(discipline => discipline.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(discipline => discipline.Updated));
        }
    }
}
