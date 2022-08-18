using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Groups.Commands.UpdateGroup
{
    public class UpdateGroupCommand : IRequest<GroupViewModel>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }
    }
}
