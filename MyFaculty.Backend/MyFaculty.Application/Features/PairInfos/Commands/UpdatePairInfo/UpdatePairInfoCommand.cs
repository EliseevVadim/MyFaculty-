using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.PairInfos.Commands.UpdatePairInfo
{
    public class UpdatePairInfoCommand : IRequest<PairInfoViewModel>
    {
        public int Id { get; set; }
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
