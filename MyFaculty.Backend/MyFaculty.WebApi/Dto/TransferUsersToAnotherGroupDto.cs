using AutoMapper;
using MyFaculty.Application.Common.Mappings;
using MyFaculty.Application.Features.Users.Commands.TransferUsersToAnotherGroup;

namespace MyFaculty.WebApi.Dto
{
    public class TransferUsersToAnotherGroupDto : IMapWith<TransferUsersToAnotherGroupCommand>
    {
        public int SourceGroupId { get; set; }
        public int DestinationGroupId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<TransferUsersToAnotherGroupDto, TransferUsersToAnotherGroupCommand>()
                .ForMember(command => command.SourceGroupId, options => options.MapFrom(dto => dto.SourceGroupId))
                .ForMember(command => command.DestinationGroupId, options => options.MapFrom(dto => dto.DestinationGroupId));
        }
    }
}
