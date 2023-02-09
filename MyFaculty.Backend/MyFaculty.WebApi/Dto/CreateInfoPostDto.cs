using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InfoPosts.Commands.CreateInfoPost;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class CreateInfoPostDto : IMapWith<CreateInfoPostCommand>
    {
        public string TextContent { get; set; }
        public List<IFormFile> PostAttachments { get; set; }
        public int? StudyClubId { get; set; }
        public int? InfoPublicId { get; set; }
        public int AuthorId { get; set; }
        public bool CommentsAllowed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateInfoPostDto, CreateInfoPostCommand>()
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId))
                .ForMember(command => command.InfoPublicId, options => options.MapFrom(dto => dto.InfoPublicId))
                .ForMember(command => command.AuthorId, options => options.MapFrom(dto => dto.AuthorId))
                .ForMember(command => command.CommentsAllowed, options => options.MapFrom(dto => dto.CommentsAllowed));
        }
    }
}
