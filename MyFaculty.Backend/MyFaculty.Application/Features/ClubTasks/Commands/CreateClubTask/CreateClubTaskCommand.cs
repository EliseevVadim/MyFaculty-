using MediatR;
using MyFaculty.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int TimezoneOffset { get; set; }
    }
}
