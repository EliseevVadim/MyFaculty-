using MediatR;
using MyFaculty.Application.ViewModels;
using System;

namespace MyFaculty.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserViewModel>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? CityId { get; set; }
        public int? FacultyId { get; set; }
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }
        public string Website { get; set; }
        public string VKLink { get; set; }
        public string TelegramLink { get; set; }
    }
}
