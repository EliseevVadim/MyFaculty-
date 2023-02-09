﻿using AutoMapper;
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
    public class InfoPostViewModel : PostViewModel, IMapWith<InfoPost>
    {
        public int? StudyClubId { get; set; }
        public int? InfoPublicId { get; set; }
        public List<UserLookupDto> LikedUsers { get; set; }
        public bool CommentsAllowed { get; set; }

        public new void Mapping(Profile profile)
        {
            profile.CreateMap<InfoPost, InfoPostViewModel>()
                .IncludeBase<Post, PostViewModel>()
                .ForMember(vm => vm.StudyClubId, options => options.MapFrom(infoPost => infoPost.StudyClubId))
                .ForMember(vm => vm.InfoPublicId, options => options.MapFrom(infoPost => infoPost.InfoPublicId))
                .ForMember(vm => vm.LikedUsers, options => options.MapFrom(infoPost => infoPost.LikedUsers))
                .ForMember(vm => vm.CommentsAllowed, options => options.MapFrom(infoPost => infoPost.CommentsAllowed));
        }
    }
}
