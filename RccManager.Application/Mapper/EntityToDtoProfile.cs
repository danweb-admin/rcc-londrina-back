using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;

namespace RccManager.Application.Mapper;

public class EntityToDtoProfile : Profile
{
    public EntityToDtoProfile()
    {
        CreateMap<DecanatoSetorDtoResult, DecanatoSetor>()
            .ReverseMap();

        CreateMap<ParoquiaCapelaDtoResult, ParoquiaCapela>()
            .ReverseMap();

        CreateMap<UserDtoAdd, User>()
            .ReverseMap();

        CreateMap<UserDtoResult, User>()
            .ReverseMap();

        CreateMap<UserDto, User>()
            .ReverseMap();
    }
}
