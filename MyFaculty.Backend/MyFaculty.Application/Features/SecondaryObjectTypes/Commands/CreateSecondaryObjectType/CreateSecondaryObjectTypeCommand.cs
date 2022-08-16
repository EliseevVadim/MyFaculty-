using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType
{
    public class CreateSecondaryObjectTypeCommand : IRequest<SecondaryObjectType>
    {
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }
    }
}
