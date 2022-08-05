using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Domain.Entities
{
    public class SecondaryObject
    {
        public Guid Id { get; set; }
        public string ObjectName { get; set; }
        public string PositionInfo { get; set; }
        public Guid SecondaryObjectTypeId { get; set; }
        public SecondaryObjectType SecondaryObjectType { get; set; }
        public Guid FloorId { get; set; }
        public Floor Floor { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
