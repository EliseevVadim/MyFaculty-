using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.InfoPosts.Commands.LikeInfoPost;

namespace MyFaculty.WebApi.Dto
{
    public class LikeInfoPostDto : IMapWith<LikeInfoPostCommand>
    {
        public int LikedUserId { get; set; }
        public int LikedPostId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LikeInfoPostDto, LikeInfoPostCommand>()
                .ForMember(command => command.LikedPostId, options => options.MapFrom(dto => dto.LikedPostId))
                .ForMember(command => command.LikedUserId, options => options.MapFrom(dto => dto.LikedUserId));
        }
    }
}
