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
    public class StudyClubViewModel : IMapWith<StudyClub>
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
        public UserLookupDto Owner { get; set; }
        public int MembersCount { get; set; }
        public List<UserLookupDto> Members { get; set; }
        public List<UserLookupDto> Moderators { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyClub, StudyClubViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(club => club.Id))
                .ForMember(vm => vm.ClubName, options => options.MapFrom(club => club.ClubName))
                .ForMember(vm => vm.Description, options => options.MapFrom(club => club.Description))
                .ForMember(vm => vm.ImagePath, options => options.MapFrom(club => club.ImagePath))
                .ForMember(vm => vm.OwnerId, options => options.MapFrom(club => club.OwnerId))
                .ForMember(vm => vm.Owner, options => options.MapFrom(club => club.Owner))
                .ForMember(vm => vm.MembersCount, options => options.MapFrom(club => club.Members.Count))
                .ForMember(vm => vm.Members, options => options.MapFrom(club => club.Members))
                .ForMember(vm => vm.Moderators, options => options.MapFrom(club => club.Moderators))
                .ForMember(vm => vm.Created, options => options.MapFrom(club => club.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(club => club.Updated));
        }
    }
}
