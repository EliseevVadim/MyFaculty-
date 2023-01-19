using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Dto
{
    public class StudyClubLookupDto : IMapWith<StudyClub>
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string ImagePath { get; set; }
        public List<UserLookupDto> Moderators { get; set; }
        public int MembersCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StudyClub, StudyClubLookupDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(club => club.Id))
                .ForMember(dto => dto.ClubName, options => options.MapFrom(club => club.ClubName))
                .ForMember(dto => dto.ImagePath, options => options.MapFrom(club => club.ImagePath))
                .ForMember(dto => dto.Moderators, options => options.MapFrom(club => club.Moderators))
                .ForMember(dto => dto.MembersCount, options => options.MapFrom(club => club.Members.Count));
        }
    }
}
