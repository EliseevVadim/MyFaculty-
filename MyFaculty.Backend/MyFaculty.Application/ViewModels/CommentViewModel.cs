using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class CommentViewModel : IMapWith<Comment>
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public Guid CommentAttachmentsUid { get; set; }
        public string Attachments { get; set; }
        public int AuthorId { get; set; }
        public UserLookupDto Author { get; set; }
        public int? ParentCommentId { get; set; }
        public RepliedCommentDto ParentComment { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(comment => comment.Id))
                .ForMember(vm => vm.TextContent, options => options.MapFrom(comment => comment.TextContent))
                .ForMember(vm => vm.CommentAttachmentsUid, options => options.MapFrom(comment => comment.CommentAttachmentsUid))
                .ForMember(vm => vm.Attachments, options => options.MapFrom(comment => comment.Attachments))
                .ForMember(vm => vm.AuthorId, options => options.MapFrom(comment => comment.AuthorId))
                .ForMember(vm => vm.Author, options => options.MapFrom(comment => comment.Author))
                .ForMember(vm => vm.ParentCommentId, options => options.MapFrom(comment => comment.ParentCommentId))
                .ForMember(vm => vm.ParentComment, options => options.MapFrom(comment => comment.ParentComment))
                .ForMember(vm => vm.Created, options => options.MapFrom(comment => comment.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(comment => comment.Updated));
        }
    }
}
