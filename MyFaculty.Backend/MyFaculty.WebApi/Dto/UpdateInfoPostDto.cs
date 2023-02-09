using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InfoPosts.Commands.UpdateInfoPost;
using System;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateInfoPostDto : IMapWith<UpdateInfoPostCommand>
    {
        public int InfoPostId { get; set; }
        public int IssuerId { get; set; }
        public Guid PostAttachmentsUid { get; set; }
        public string TextContent { get; set; }
        public string OldAttachments { get; set; }
        public string ActualAttachments { get; set; }
        public List<IFormFile> NewFiles { get; set; }
        public bool CommentsAllowed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateInfoPostDto, UpdateInfoPostCommand>()
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.CommentsAllowed, options => options.MapFrom(dto => dto.CommentsAllowed));
        }
    }
}
