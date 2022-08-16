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
    public class TeacherViewModel : IMapWith<Teacher>
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int ScienceRankId { get; set; }
        public string ScienceRankName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Teacher, TeacherViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(teacher => teacher.Id))
                .ForMember(vm => vm.FIO, options => options.MapFrom(teacher => teacher.FIO))
                .ForMember(vm => vm.PhotoPath, options => options.MapFrom(teacher => teacher.PhotoPath))
                .ForMember(vm => vm.Email, options => options.MapFrom(teacher => teacher.Email))
                .ForMember(vm => vm.BirthDate, options => options.MapFrom(teacher => teacher.BirthDate))
                .ForMember(vm => vm.ScienceRankId, options => options.MapFrom(teacher => teacher.ScienceRankId))
                .ForMember(vm => vm.ScienceRankName, options => options.MapFrom(teacher => teacher.ScienceRank.RankName))
                .ForMember(vm => vm.Created, options => options.MapFrom(teacher => teacher.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(teacher => teacher.Updated));
        }
    }
}
