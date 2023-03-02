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
    public class PostViewModel : IMapWith<Post>
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public Guid PostAttachmentsUid { get; set; }
        public string Attachments { get; set; }
        public int CommentsCount { get; set; }
        public int AuthorId { get; set; }
        public UserLookupDto Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(post => post.Id))
                .ForMember(vm => vm.TextContent, options => options.MapFrom(post => post.TextContent))
                .ForMember(vm => vm.Attachments, options => options.MapFrom(post => post.Attachments))
                .ForMember(vm => vm.PostAttachmentsUid, options => options.MapFrom(post => post.PostAttachmentsUid))
                .ForMember(vm => vm.CommentsCount, options => options.MapFrom(post => post.Comments.Count))
                .ForMember(vm => vm.AuthorId, options => options.MapFrom(post => post.AuthorId))
                .ForMember(vm => vm.Author, options => options.MapFrom(post => post.Author))
                .ForMember(vm => vm.Created, options => options.MapFrom(post => post.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(post => post.Updated));
        }
    }
}
