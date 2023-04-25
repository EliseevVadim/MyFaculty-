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
    public class PostOwnerViewModel
    {
        public string OwnerName { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerLink { get; set; }
        public List<int> ModeratorsIds { get; set; }
    }
}
