using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.ParoquiaCapela;
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
    }
}
