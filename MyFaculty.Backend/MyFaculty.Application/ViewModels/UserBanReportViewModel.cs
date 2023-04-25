using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class UserBanReportViewModel : IMapWith<UserBanReport>
    {
        public int Id { get; set; }
        public string AffectedUserLink { get; set; }
        public string AdministratorLink { get; set; }
        public UserBanAction PerformedAction { get; set; }
        public string Reason { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserBanReport, UserBanReportViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(banReport => banReport.Id))
                .ForMember(vm => vm.AffectedUserLink, options => options.MapFrom(banReport => $"/id{banReport.AffectedUserId}"))
                .ForMember(vm => vm.AdministratorLink, options => options.MapFrom(banReport => $"/id{banReport.AdministratorId}"))
                .ForMember(vm => vm.PerformedAction, options => options.MapFrom(banReport => banReport.PerformedAction))
                .ForMember(vm => vm.Reason, options => options.MapFrom(banReport => banReport.Reason))
                .ForMember(vm => vm.Created, options => options.MapFrom(banReport => banReport.Created));
        }
    }
}
