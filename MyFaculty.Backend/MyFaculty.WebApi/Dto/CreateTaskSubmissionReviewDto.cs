using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.TaskSubmissionReviews.Commands.CreateTaskSubmissionReview;
using System;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class CreateTaskSubmissionReviewDto : IMapWith<CreateTaskSubmissionReviewCommand>
    {
        public string TextContent { get; set; }
        public List<IFormFile> Attachments { get; set; }
        public Guid SubmissionReviewAttachmentsUid { get; set; }
        public int Rate { get; set; }
        public int SubmissionId { get; set; }
        public int ReviewerId { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTaskSubmissionReviewDto, CreateTaskSubmissionReviewCommand>()
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.SubmissionReviewAttachmentsUid, options => options.MapFrom(dto => dto.SubmissionReviewAttachmentsUid))
                .ForMember(command => command.Rate, options => options.MapFrom(dto => dto.Rate))
                .ForMember(command => command.SubmissionId, options => options.MapFrom(dto => dto.SubmissionId))
                .ForMember(command => command.ReviewerId, options => options.MapFrom(dto => dto.ReviewerId));
        }
    }
}
