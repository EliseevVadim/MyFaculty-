using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class TaskSubmissionViewModel : IMapWith<TaskSubmission>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public Guid SubmissionAttachmentsUid { get; set; }
        public string Attachments { get; set; }
        public TaskSubmissionStatus Status { get; set; }
        public TaskSubmissionReviewViewModel Review { get; set; }
        public UserLookupDto Author { get; set; }
        public int MaxRate { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TaskSubmission, TaskSubmissionViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(submission => submission.Id))
                .ForMember(vm => vm.Title, options => options.MapFrom(submission => submission.Title))
                .ForMember(vm => vm.TextContent, options => options.MapFrom(submission => submission.TextContent))
                .ForMember(vm => vm.SubmissionAttachmentsUid, options => options.MapFrom(submission => submission.SubmissionAttachmentsUid))
                .ForMember(vm => vm.Attachments, options => options.MapFrom(submission => submission.Attachments))
                .ForMember(vm => vm.Status, options => options.MapFrom(submission => submission.Status))
                .ForMember(vm => vm.Review, options => options.MapFrom(submission => submission.Review))
                .ForMember(vm => vm.Author, options => options.MapFrom(submission => submission.Author))
                .ForMember(vm => vm.MaxRate, options => options.MapFrom(submission => submission.ClubTask.Cost))
                .ForMember(vm => vm.Created, options => options.MapFrom(submission => submission.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(submission => submission.Updated));
        }
    }
}
