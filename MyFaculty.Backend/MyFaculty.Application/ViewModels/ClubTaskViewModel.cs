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
    public class ClubTaskViewModel : PostViewModel, IMapWith<ClubTask>
    {
        public int StudyClubId { get; set; }
        public string StudyClubName { get; set; }
        public string StudyClubImage { get; set; }
        public DateTime DeadLine { get; set; }
        public int Cost { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<ClubTask, ClubTaskViewModel>()
                .ForMember(e => e.Author, options => options.Ignore())
                .IncludeBase<Post, PostViewModel>()
                .ForMember(vm => vm.StudyClubId, options => options.MapFrom(task => task.StudyClubId))
                .ForMember(vm => vm.StudyClubName, options => options.MapFrom(task => task.OwningStudyClub.ClubName))
                .ForMember(vm => vm.StudyClubImage, options => options.MapFrom(task => task.OwningStudyClub.ImagePath))
                .ForMember(vm => vm.Cost, options => options.MapFrom(task => task.Cost))
                .ForMember(vm => vm.DeadLine, options => options.MapFrom(task => task.DeadLine));
        }
    }
}
