using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MyFaculty.Application.Features.Pairs.Commands.DeletePair
{
    public class DeletePairCommand : IRequest
    {
        public int Id { get; set; }
    }
}
