using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.CreateSecondaryObjectType
{
    public class CreateSecondaryObjectTypeCommand : IRequest<SecondaryObjectTypeViewModel>
    {
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }
    }
}
