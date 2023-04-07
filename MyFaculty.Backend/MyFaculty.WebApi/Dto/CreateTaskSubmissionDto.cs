using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.TaskSubmissions.Commands.CreateTaskSubmission;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class CreateTaskSubmissionDto : IMapWith<CreateTaskSubmissionCommand>
    {
        public string Title { get; set; }
        public string TextContent { get; set; }
        public List<IFormFile> SubmissionAttachments { get; set; }
        public int AuthorId { get; set; }
        public int TaskId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTaskSubmissionDto, CreateTaskSubmissionCommand>()
                .ForMember(command => command.Title, options => options.MapFrom(dto => dto.Title))
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.AuthorId, options => options.MapFrom(dto => dto.AuthorId))
                .ForMember(command => command.ClubTaskId, options => options.MapFrom(dto => dto.TaskId));
        }
    }
}
