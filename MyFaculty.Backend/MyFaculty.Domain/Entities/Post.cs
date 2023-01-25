using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public int AuthorId { get; set; }
        public AppUser Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
