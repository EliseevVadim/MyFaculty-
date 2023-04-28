using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteAllInformationPublicContent
{
    public class DeleteAllInformationPublicContentCommand : IRequest<List<string>>
    {
        public int PublicId { get; set; }
    }
}
