using AutoMapper;
using MediatR;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Auditoriums.Commands.CreateAuditorium
{
    public class CreateAuditoriumCommandHandler : IRequestHandler<CreateAuditoriumCommand, AuditoriumViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public CreateAuditoriumCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuditoriumViewModel> Handle(CreateAuditoriumCommand request, CancellationToken cancellationToken)
        {
            Auditorium auditorium = new Auditorium()
            {
                AuditoriumName = request.AuditoriumName,
                PositionInfo = request.PositionInfo,
                FloorId = request.FloorId,
                TeacherId = request.TeacherId,
                Created = DateTime.Now
            };
            await _context.Auditoriums.AddAsync(auditorium, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AuditoriumViewModel>(auditorium);
        }
    }
}
