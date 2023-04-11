using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.TaskSubmissionReviews.Commands.UpdateTaskSubmissionReview;
using System;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateTaskSubmissionReviewDto : IMapWith<UpdateTaskSubmissionReviewCommand>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public Guid SubmissionReviewAttachmentsUid { get; set; }
        public string TextContent { get; set; }
        public string OldAttachments { get; set; }
        public string ActualAttachments { get; set; }
        public List<IFormFile> NewFiles { get; set; }
        public int Rate { get; set; }
        public int NewStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTaskSubmissionReviewDto, UpdateTaskSubmissionReviewCommand>()
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.Rate, options => options.MapFrom(dto => dto.Rate))
                .ForMember(command => command.NewStatus, options => options.MapFrom(dto => dto.NewStatus));
        }
    }
}
