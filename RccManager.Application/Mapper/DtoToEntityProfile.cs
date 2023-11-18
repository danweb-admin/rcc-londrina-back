using System;
using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Dtos.FormacoesServo;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;

namespace RccManager.Application.Mapper;

public class DtoToEntityProfile : Profile
{
    public DtoToEntityProfile()
    {
        CreateMap<DecanatoSetor, DecanatoSetorDto>()
            .ReverseMap();

        CreateMap<ParoquiaCapela, ParoquiaCapelaDto>()
            .ReverseMap();

        CreateMap<GrupoOracao, GrupoOracaoDto>()
            .ReverseMap();

        CreateMap<GrupoOracao, GrupoOracaoDtoResult>()
            .ReverseMap();

        CreateMap<Servo, ServoDto>()
            .ReverseMap();

        CreateMap<Servo, ServoDtoResult>()
            .ReverseMap();

        CreateMap<Formacao, FormacaoDto>()
            .ReverseMap();

        CreateMap<FormacoesServo, FormacoesServoDtoResult>()
            .ReverseMap();

        CreateMap<User, UserDto>()
            .ReverseMap();

        CreateMap<User, UserDtoAdd>()
            .ReverseMap();

        CreateMap<User, UserDtoResult>()
            .ReverseMap();
    }
}
