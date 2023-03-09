using AutoMapper;
using Microsoft.AspNetCore.Http;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.ClubTasks.Commands.CreateClubTask;
using System;
using System.Collections.Generic;

namespace MyFaculty.WebApi.Dto
{
    public class CreateClubTaskDto : IMapWith<CreateClubTaskCommand>
    {
        public string TextContent { get; set; }
        public List<IFormFile> PostAttachments { get; set; }
        public int StudyClubId { get; set; }
        public int AuthorId { get; set; }
        public DateTime DeadLine { get; set; }
        public int Cost { get; set; }
        public int TimezoneOffset { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateClubTaskDto, CreateClubTaskCommand>()
                .ForMember(command => command.TextContent, options => options.MapFrom(dto => dto.TextContent))
                .ForMember(command => command.StudyClubId, options => options.MapFrom(dto => dto.StudyClubId))
                .ForMember(command => command.AuthorId, options => options.MapFrom(dto => dto.AuthorId))
                .ForMember(command => command.DeadLine, options => options.MapFrom(dto => dto.DeadLine))
                .ForMember(command => command.Cost, options => options.MapFrom(dto => dto.Cost))
                .ForMember(command => command.TimezoneOffset, options => options.MapFrom(dto => dto.TimezoneOffset));
        }
    }
}
