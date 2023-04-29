using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyFaculty.Application.ViewModels
{
    public class InfoPostViewModel : PostViewModel, IMapWith<InfoPost>
    {
        public int? StudyClubId { get; set; }
        public int? InfoPublicId { get; set; }
        public PostOwnerViewModel Owner { get; set; }
        public List<UserLookupDto> LikedUsers { get; set; }
        public bool CommentsAllowed { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<InfoPost, InfoPostViewModel>()
                .IncludeBase<Post, PostViewModel>()
                .ForMember(vm => vm.StudyClubId, options => options.MapFrom(infoPost => infoPost.StudyClubId))
                .ForMember(vm => vm.InfoPublicId, options => options.MapFrom(infoPost => infoPost.InfoPublicId))
                .ForMember(vm => vm.Owner, options => options.MapFrom(infoPost => infoPost.OwningInformationPublic != null ?
                new PostOwnerViewModel()
                {
                    OwnerName = infoPost.OwningInformationPublic.PublicName,
                    OwnerAvatar = infoPost.OwningInformationPublic.ImagePath,
                    OwnerLink = $"public{infoPost.OwningInformationPublic.Id}",
                    IsBanned = infoPost.OwningInformationPublic.IsBanned,
                    OwningUserId = infoPost.OwningInformationPublic.OwnerId
                } :
                new PostOwnerViewModel()
                {
                    OwnerName = infoPost.OwningStudyClub.ClubName,
                    OwnerAvatar = infoPost.OwningStudyClub.ImagePath,
                    OwnerLink = $"clubs/{infoPost.OwningStudyClub.Id}",
                    ModeratorsIds = infoPost.OwningStudyClub.Moderators.Select(user => user.Id).ToList(),
                    IsBanned = false,
                    OwningUserId = infoPost.OwningStudyClub.OwnerId
                }))
                .ForMember(vm => vm.LikedUsers, options => options.MapFrom(infoPost => infoPost.LikedUsers))
                .ForMember(vm => vm.CommentsAllowed, options => options.MapFrom(infoPost => infoPost.CommentsAllowed));
        }
    }
}
