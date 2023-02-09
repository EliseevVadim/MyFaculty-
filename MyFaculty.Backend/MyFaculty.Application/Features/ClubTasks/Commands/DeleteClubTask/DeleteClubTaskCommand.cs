using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.ClubTasks.Commands.DeleteClubTask
{
    public class DeleteClubTaskCommand : IRequest<ClubTaskViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
    }
}
