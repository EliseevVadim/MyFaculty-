using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserViewModel>
    {
        private readonly IMFDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id, cancellationToken);
            if (user == null)
                throw new EntityNotFoundException(nameof(AppUser), request.Id);
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.AvatarPath = string.IsNullOrEmpty(request.AvatarPath) ? user.AvatarPath : request.AvatarPath;
            user.CityId = request.CityId;
            user.FacultyId = request.FacultyId;
            user.CourseId = request.CourseId;
            user.GroupId = request.GroupId;
            user.BirthDate = request.BirthDate;
            user.Website = string.IsNullOrWhiteSpace(request.Website) ? null : request.Website;
            user.VKLink = string.IsNullOrWhiteSpace(request.VKLink) ? null : request.VKLink;
            user.TelegramLink = string.IsNullOrWhiteSpace(request.TelegramLink) ? null : request.TelegramLink;
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
