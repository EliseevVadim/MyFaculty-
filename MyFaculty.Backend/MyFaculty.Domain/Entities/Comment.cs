using System;
using System.Collections.Generic;

namespace MyFaculty.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public Guid CommentAttachmentsUid { get; set; }
        public string Attachments { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public int AuthorId { get; set; }
        public AppUser Author { get; set; }
        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public List<Comment> Replies { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
