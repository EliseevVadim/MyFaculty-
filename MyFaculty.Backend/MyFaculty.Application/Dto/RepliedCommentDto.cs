using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class RepliedCommentDto : IMapWith<Comment>
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public int AuthorId { get; set; }
        public UserLookupDto Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, RepliedCommentDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(comment => comment.Id))
                .ForMember(dto => dto.TextContent, options => options.MapFrom(comment => comment.TextContent))
                .ForMember(dto => dto.AuthorId, options => options.MapFrom(comment => comment.AuthorId))
                .ForMember(dto => dto.Author, options => options.MapFrom(comment => comment.Author))
                .ForMember(dto => dto.Created, options => options.MapFrom(comment => comment.Created))
                .ForMember(dto => dto.Updated, options => options.MapFrom(comment => comment.Updated));
        }
    }
}
