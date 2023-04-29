using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.StudyClubs.Commands.UpdateStudyClub
{
    public class UpdateStudyClubCommand : IRequest<StudyClubViewModel>
    {
        public int Id { get; set; }
        public int IssuerId { get; set; }
        public string StudyClubName { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int OwnerId { get; set; }
    }
}
