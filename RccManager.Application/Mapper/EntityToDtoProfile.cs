using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Servo;
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

        CreateMap<GrupoOracaoDto, GrupoOracao>()
            .ReverseMap();

        CreateMap<GrupoOracaoDtoResult, GrupoOracao>()
            .ReverseMap();

        CreateMap<ServoDto, Servo>()
            .ReverseMap();

        CreateMap<ServoDtoResult, Servo>()
            .ReverseMap();

        CreateMap<UserDtoAdd, User>()
            .ReverseMap();

        CreateMap<UserDtoResult, User>()
            .ReverseMap();

        CreateMap<UserDto, User>()
            .ReverseMap();
    }
}
