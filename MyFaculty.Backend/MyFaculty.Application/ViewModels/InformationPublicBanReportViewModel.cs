using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using MyFaculty.Domain.Enums;
using System;

namespace MyFaculty.Application.ViewModels
{
    public class InformationPublicBanReportViewModel : IMapWith<InformationPublicBanReport>
    {
        public int Id { get; set; }
        public string PublicLink { get; set; }
        public string AdministratorLink { get; set; }
        public BanAction PerformedAction { get; set; }
        public string Reason { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InformationPublicBanReport, InformationPublicBanReportViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(banReport => banReport.Id))
                .ForMember(vm => vm.PublicLink, options => options.MapFrom(banReport => $"/public{banReport.PublicId}"))
                .ForMember(vm => vm.AdministratorLink, options => options.MapFrom(banReport => $"/id{banReport.AdministratorId}"))
                .ForMember(vm => vm.PerformedAction, options => options.MapFrom(banReport => banReport.PerformedAction))
                .ForMember(vm => vm.Reason, options => options.MapFrom(banReport => banReport.Reason))
                .ForMember(vm => vm.Created, options => options.MapFrom(banReport => banReport.Created));
        }
    }
}
