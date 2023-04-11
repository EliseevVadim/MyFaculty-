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
    public class ClubTaskViewModel : PostViewModel, IMapWith<ClubTask>
    {
        public StudyClubLookupDto OwningStudyClub { get; set; }
        public string StudyClubLink { get; set; }
        public DateTime DeadLine { get; set; }
        public int Cost { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<ClubTask, ClubTaskViewModel>()
                .ForMember(e => e.Author, options => options.Ignore())
                .IncludeBase<Post, PostViewModel>()
                .ForMember(vm => vm.OwningStudyClub, options => options.MapFrom(task => task.OwningStudyClub))
                .ForMember(vm => vm.StudyClubLink, options => options.MapFrom(task => $"club/{task.OwningStudyClub.Id}"))
                .ForMember(vm => vm.Cost, options => options.MapFrom(task => task.Cost))
                .ForMember(vm => vm.DeadLine, options => options.MapFrom(task => task.DeadLine));
        }
    }
}
