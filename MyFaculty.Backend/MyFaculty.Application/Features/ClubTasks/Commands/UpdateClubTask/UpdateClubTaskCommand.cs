using MediatR;
using MyFaculty.Application.ViewModels;
using System;

namespace MyFaculty.Application.Features.ClubTasks.Commands.UpdateClubTask
{
    public class UpdateClubTaskCommand : IRequest<ClubTaskViewModel>
    {
        public int TaskId { get; set; }
        public int IssuerId { get; set; }
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public DateTime DeadLine { get; set; }
        public int Cost { get; set; }
        public int TimezoneOffset { get; set; }
    }
}