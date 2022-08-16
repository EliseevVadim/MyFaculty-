using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFaculty.Application.ViewModels;
using MediatR;

namespace MyFaculty.Application.Features.Floors.Queries.GetFloors
{
    public class GetFloorsQuery : IRequest<FloorsListViewModel>
    {
    }
}
