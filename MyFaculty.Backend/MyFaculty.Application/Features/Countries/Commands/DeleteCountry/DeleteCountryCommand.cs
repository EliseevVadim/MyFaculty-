﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommand : IRequest
    {
        public int Id { get; set; }
    }
}
