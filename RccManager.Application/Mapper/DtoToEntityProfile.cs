using System;
using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Dtos.FormacoesServo;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Service.Helper;

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
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextDecrypter()));

        CreateMap<Servo, ServoDtoResult>()
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextDecrypter()));

        CreateMap<ServoTemp, ServoTempDto>()
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextDecrypter()));

        CreateMap<ServoTemp, ServoTempDtoResult>()
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextDecrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextDecrypter()));

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

    public class TextDecrypter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(sourceMember))
                return string.Empty;
            return Utils.Decrypt(sourceMember);
        }
    }
}
