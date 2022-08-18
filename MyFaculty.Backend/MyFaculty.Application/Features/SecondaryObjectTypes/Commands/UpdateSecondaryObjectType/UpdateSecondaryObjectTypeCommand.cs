using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.SecondaryObjectTypes.Commands.UpdateSecondaryObjectType
{
    public class UpdateSecondaryObjectTypeCommand : IRequest<SecondaryObjectTypeViewModel>
    {
        public int Id { get; set; }
        public string ObjectTypeName { get; set; }
        public string TypePath { get; set; }
    }
}
