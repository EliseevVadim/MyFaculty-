using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.InfoPosts.Queries.GetInfoPost
{
    public class GetInfoPostQueryHandler : IRequestHandler<GetInfoPostQuery, InfoPostViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetInfoPostQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InfoPostViewModel> Handle(GetInfoPostQuery request, CancellationToken cancellationToken)
        {
            InfoPost infoPost = await _context.InfoPosts.FirstOrDefaultAsync(infoPost => infoPost.Id == request.Id, cancellationToken);
            if (infoPost == null)
                throw new EntityNotFoundException(nameof(InfoPost), request.Id);
            return _mapper.Map<InfoPostViewModel>(infoPost);
        }
    }
}
