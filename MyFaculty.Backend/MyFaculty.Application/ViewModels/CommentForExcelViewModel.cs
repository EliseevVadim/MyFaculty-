using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class CommentForExcelViewModel : IMapWith<Comment>
    {
        [Description("Id автора")]
        public string AuthorId { get; set; }
        [Description("Имя")]
        public string AuthorsFirstName { get; set; }
        [Description("Фамилия")]
        public string AuthorsLastName { get; set; }
        [Description("Текст комментария")]
        public string CommentText { get; set; }
        [Description("Дата создания")]
        public string CommentCreated { get; set; }
        [Description("Дата обновления")]
        public string CommentUpdated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentForExcelViewModel>()
                .ForMember(vm => vm.AuthorId, options => options.MapFrom(comment => $"/id{comment.AuthorId}"))
                .ForMember(vm => vm.AuthorsFirstName, options => options.MapFrom(comment => comment.Author.FirstName))
                .ForMember(vm => vm.AuthorsLastName, options => options.MapFrom(comment => comment.Author.LastName))
                .ForMember(vm => vm.CommentText, options => options.MapFrom(comment => Regex.Replace(comment.TextContent, "<.*?>", string.Empty)))
                .ForMember(vm => vm.CommentCreated, options => options.MapFrom(comment => comment.Created.ToString()))
                .ForMember(vm => vm.CommentUpdated, options => options.MapFrom(comment => comment.Updated.ToString()));
        }
    }
}
