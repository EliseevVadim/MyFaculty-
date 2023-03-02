using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Comments.Commands.UpdateComment;
using System;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateCommentDto : IMapWith<UpdateCommentCommand>
    {
        public int CommentId { get; set; }
        public int IssuerId { get; set; }
        public int? ParentCommentId { get; set; }
        public Guid CommentAttachmentsUid { get; set; }
        public string TextContent { get; set; }
        public string OldAttachments { get; set; }
        public string ActualAttachments { get; set; }
        public List<IFormFile> NewFiles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCommentDto, UpdateCommentCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.CommentId))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.ParentCommentId, options => options.MapFrom(dto => dto.ParentCommentId))
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent));
        }
    }
}
