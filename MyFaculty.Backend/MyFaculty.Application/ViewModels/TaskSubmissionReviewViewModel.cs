using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Dto;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class TaskSubmissionReviewViewModel : IMapWith<TaskSubmissionReview>
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public Guid SubmissionReviewAttachmentsUid { get; set; }
        public string Attachments { get; set; }
        public int Rate { get; set; }
        public UserLookupDto Reviewer { get; set; }
        public int SubmissionId { get; set; }
        public int SubmissionAuthorId { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TaskSubmissionReview, TaskSubmissionReviewViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(review => review.Id))
                .ForMember(vm => vm.TextContent, options => options.MapFrom(review => review.TextContent))
                .ForMember(vm => vm.SubmissionReviewAttachmentsUid, options => options.MapFrom(review => review.SubmissionReviewAttachmentsUid))
                .ForMember(vm => vm.Attachments, options => options.MapFrom(review => review.Attachments))
                .ForMember(vm => vm.Rate, options => options.MapFrom(review => review.Rate))
                .ForMember(vm => vm.Reviewer, options => options.MapFrom(review => review.Reviewer))
                .ForMember(vm => vm.SubmissionId, options => options.MapFrom(review => review.SubmissionId))
                .ForMember(vm => vm.SubmissionAuthorId, options => options.MapFrom(review => review.TaskSubmission.AuthorId))
                .ForMember(vm => vm.Created, options => options.MapFrom(review => review.Created))
                .ForMember(vm => vm.Updated, options => options.MapFrom(review => review.Updated));
        }
    }
}
