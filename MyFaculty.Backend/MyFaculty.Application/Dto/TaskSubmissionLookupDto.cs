using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class TaskSubmissionLookupDto : IMapWith<TaskSubmission>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TaskSubmissionStatus Status { get; set; }
        public int AuthorId { get; set; }
        public string AuthorFullName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TaskSubmission, TaskSubmissionLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(submission => submission.Id))
                .ForMember(dto => dto.Title, options => options.MapFrom(submission => submission.Title))
                .ForMember(dto => dto.Status, options => options.MapFrom(submission => submission.Status))
                .ForMember(dto => dto.AuthorId, options => options.MapFrom(submission => submission.AuthorId))
                .ForMember(dto => dto.AuthorFullName, 
                    options => options.MapFrom(submission => string.Concat(new string[] {submission.Author.FirstName, " ", submission.Author.LastName})))
                .ForMember(dto => dto.Created, options => options.MapFrom(submission => submission.Created))
                .ForMember(dto => dto.Updated, options => options.MapFrom(submission => submission.Updated));
        }
    }
}
