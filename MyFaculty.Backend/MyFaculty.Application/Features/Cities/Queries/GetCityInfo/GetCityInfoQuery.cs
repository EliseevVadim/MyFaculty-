using MediatR;
using MyFaculty.Application.ViewModels;

namespace MyFaculty.Application.Features.Cities.Queries.GetCityInfo
{
    public class GetCityInfoQuery : IRequest<CityViewModel>
    {
        public int Id { get; set; }
    }
}
