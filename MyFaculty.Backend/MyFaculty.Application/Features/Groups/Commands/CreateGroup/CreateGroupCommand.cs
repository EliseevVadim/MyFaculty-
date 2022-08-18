using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Groups.Commands.CreateGroup
{
    public class CreateGroupCommand : IRequest<GroupViewModel>
    {
        public string GroupName { get; set; }
        public int CourseId { get; set; }
    }
}
