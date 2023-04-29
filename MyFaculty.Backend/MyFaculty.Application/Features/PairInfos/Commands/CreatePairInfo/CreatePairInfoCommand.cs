using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.PairInfos.Commands.CreatePairInfo
{
    public class CreatePairInfoCommand : IRequest<PairInfoViewModel>
    {
        public int PairNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
