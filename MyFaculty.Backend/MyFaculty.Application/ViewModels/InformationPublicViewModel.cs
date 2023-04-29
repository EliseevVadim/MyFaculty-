using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;

namespace MyFaculty.Application.ViewModels
{
    public class InformationPublicViewModel : IMapWith<InformationPublic>
    {
        public int Id { get; set; }
        public string PublicName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
        public bool IsBanned { get; set; }
        public UserLookupDto Owner { get; set; }
        public int MembersCount { get; set; }
        public List<UserLookupDto> Members { get; set; }
        public List<UserLookupDto> Moderators { get; set; }
        public List<UserLookupDto> BlockedUsers { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InformationPublic, InformationPublicViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(infoPublic => infoPublic.Id))
                .ForMember(vm => vm.PublicName, options => options.MapFrom(infoPublic => infoPublic.PublicName))
                .ForMember(vm => vm.Description, options => options.MapFrom(infoPublic => infoPublic.Description))
                .ForMember(vm => vm.ImagePath, options => options.MapFrom(infoPublic => infoPublic.ImagePath))
                .ForMember(vm => vm.OwnerId, options => options.MapFrom(infoPublic => infoPublic.OwnerId))
                .ForMember(vm => vm.IsBanned, options => options.MapFrom(infoPublic => infoPublic.IsBanned))
                .ForMember(vm => vm.Owner, options => options.MapFrom(infoPublic => infoPublic.Owner))
                .ForMember(vm => vm.MembersCount, options => options.MapFrom(infoPublic => infoPublic.Members.Count))
                .ForMember(vm => vm.Members, options => options.MapFrom(infoPublic => infoPublic.Members))
                .ForMember(vm => vm.Moderators, options => options.MapFrom(infoPublic => infoPublic.Moderators))
                .ForMember(vm => vm.BlockedUsers, options => options.MapFrom(infoPublic => infoPublic.BlockedUsers))
                .ForMember(vm => vm.Created, options => options.MapFrom(infoPublic => infoPublic.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(infoPublic => infoPublic.Updated));
        }
    }
}
