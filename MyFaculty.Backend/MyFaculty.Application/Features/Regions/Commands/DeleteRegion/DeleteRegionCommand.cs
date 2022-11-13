using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Regions.Commands.DeleteRegion
{
    public class DeleteRegionCommand : IRequest
    {
        public int Id { get; set; }
    }
}
