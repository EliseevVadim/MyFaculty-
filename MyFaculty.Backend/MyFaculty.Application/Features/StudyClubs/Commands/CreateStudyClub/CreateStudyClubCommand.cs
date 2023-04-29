using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.StudyClubs.Commands.CreateStudyClub
{
    public class CreateStudyClubCommand : IRequest<StudyClubViewModel>
    {
        public string StudyClubName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
    }
}
