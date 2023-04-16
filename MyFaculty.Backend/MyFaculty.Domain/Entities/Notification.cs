using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TextContent { get; set; }
        public string ReturnUrl { get; set; }
        public string MetaInfo { get; set; }
        public AppUser NotifiedUser { get; set; }
        public DateTime Created { get; set; }
    }
}
