using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.DeleteSecondaryObjectType
{
    public class DeleteSecondaryObjectTypeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
