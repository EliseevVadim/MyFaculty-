﻿using MyFaculty.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.ViewModels
{
    public class GroupsListViewModel
    {
        public IList<GroupLookupDto> Groups { get; set; }
    }
}
