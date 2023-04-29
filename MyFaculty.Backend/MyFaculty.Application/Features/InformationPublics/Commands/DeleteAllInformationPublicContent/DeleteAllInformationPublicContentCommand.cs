using MediatR;
using System.Collections.Generic;

namespace MyFaculty.Application.Features.InformationPublics.Commands.DeleteAllInformationPublicContent
{
    public class DeleteAllInformationPublicContentCommand : IRequest<List<string>>
    {
        public int PublicId { get; set; }
    }
}
