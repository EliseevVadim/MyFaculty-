using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.SecondaryObjects.Queries.GetSecondaryObjectInfo
{
    public class GetSecondaryObjectInfoQuery : IRequest<SecondaryObjectViewModel>
    {
        public int Id { get; set; }
    }
}
