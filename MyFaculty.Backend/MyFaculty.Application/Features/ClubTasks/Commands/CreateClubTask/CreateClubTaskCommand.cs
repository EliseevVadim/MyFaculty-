using MediatR;
using MyFaculty.Application.ViewModels;
using System;

namespace MyFaculty.Application.Features.ClubTasks.Commands.CreateClubTask
{
    public class CreateClubTaskCommand : IRequest<ClubTaskViewModel>
    {
        public string TextContent { get; set; }
        public string Attachments { get; set; }
        public Guid PostAttachmentsUid { get; set; }
        public int StudyClubId { get; set; }
        public int AuthorId { get; set; }
        public DateTime DeadLine { get; set; }
        public int Cost { get; set; }
        public int TimezoneOffset { get; set; }
    }
}
