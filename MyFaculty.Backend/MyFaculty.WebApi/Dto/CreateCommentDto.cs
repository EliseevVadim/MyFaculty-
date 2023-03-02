using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Comments.Commands.CreateComment;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class CreateCommentDto : IMapWith<CreateCommentCommand>
    {
        public string TextContent { get; set; }
        public List<IFormFile> CommentAttachments { get; set; }
        public int AuthorId { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCommentDto, CreateCommentCommand>()
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.AuthorId, options => options.MapFrom(dto => dto.AuthorId))
                .ForMember(command => command.PostId, options => options.MapFrom(dto => dto.PostId))
                .ForMember(command => command.ParentCommentId, options => options.MapFrom(dto => dto.ParentCommentId));
        }
    }
}
