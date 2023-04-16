using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class NotificationViewModel : IMapWith<Notification>
    {
        public int Id { get; set; }
        public string TextContent { get; set; }
        public string ReturnUrl { get; set; }
        public string MetaInfo { get; set; }
        public DateTime Created { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Notification, NotificationViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(notification => notification.Id))
                .ForMember(vm => vm.TextContent, options => options.MapFrom(notification => notification.TextContent))
                .ForMember(vm => vm.ReturnUrl, options => options.MapFrom(notification => notification.ReturnUrl))
                .ForMember(vm => vm.MetaInfo, options => options.MapFrom(notification => notification.MetaInfo))
                .ForMember(vm => vm.Created, options => options.MapFrom(notification => notification.Created));
        }
    }
}
