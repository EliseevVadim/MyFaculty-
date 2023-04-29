using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.WorkDaysOfWeek.Queries.GetWorkDaysOfWeek
{
    public class GetWorkDaysOfWeekQuery : IRequest<WorkDaysOfWeekListViewModel>
    {

    }
}
