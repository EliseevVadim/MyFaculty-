using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.PairInfos.Queries.GetPairInfoDetails
{
    public class GetPairInfoDetailsQueryHandler : IRequestHandler<GetPairInfoDetailsQuery, PairInfoViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetPairInfoDetailsQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PairInfoViewModel> Handle(GetPairInfoDetailsQuery request, CancellationToken cancellationToken)
        {
            PairInfo pairInfo = await _context.PairInfos.FirstOrDefaultAsync(info => info.Id == request.Id, cancellationToken);
            if (pairInfo == null)
                throw new EntityNotFoundException(nameof(PairInfo), request.Id);
            return _mapper.Map<PairInfoViewModel>(pairInfo);
        }
    }
}
