using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.TaskSubmissions.Commands.UpdateTaskSubmission;
using System;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateTaskSubmissionDto : IMapWith<UpdateTaskSubmissionCommand>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public Guid SubmissionAttachmentsUid { get; set; }
        public string Title { get; set; }
        public string TextContent { get; set; }
        public string OldAttachments { get; set; }
        public string ActualAttachments { get; set; }
        public List<IFormFile> NewFiles { get; set; }
        public string Status { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateTaskSubmissionDto, UpdateTaskSubmissionCommand>()
                .ForMember(command => command.Title, options => options.MapFrom(dto => dto.Title))
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.Id, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.Status, options => options.MapFrom(dto => dto.Status));
        }
    }
}
