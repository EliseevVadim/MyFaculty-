using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.ClubTasks.Commands.UpdateClubTask;
using System;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class UpdateClubTaskDto : IMapWith<UpdateClubTaskCommand>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public Guid PostAttachmentsUid { get; set; }
        public string TextContent { get; set; }
        public string OldAttachments { get; set; }
        public string ActualAttachments { get; set; }
        public List<IFormFile> NewFiles { get; set; }
        public DateTime DeadLine { get; set; }
        public int TimezoneOffset { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateClubTaskDto, UpdateClubTaskCommand>()
                .ForMember(command => command.TaskId, options => options.MapFrom(dto => dto.Id))
                .ForMember(command => command.IssuerId, options => options.MapFrom(dto => dto.IssuerId))
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.DeadLine, options => options.MapFrom(dto => dto.DeadLine))
                .ForMember(command => command.TimezoneOffset, options => options.MapFrom(dto => dto.TimezoneOffset));
        }
    }
}
