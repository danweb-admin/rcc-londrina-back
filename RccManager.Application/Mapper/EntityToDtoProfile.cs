using AutoMapper;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Dtos.FormacoesServo;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.InscricoesEvento;
using RccManager.Domain.Dtos.ParoquiaCapela;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.ServoTemp;
using RccManager.Domain.Dtos.Users;
using RccManager.Domain.Entities;
using RccManager.Service.Helper;
using static RccManager.Application.Mapper.DtoToEntityProfile;

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
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextEncrypter()));


        CreateMap<ServoDtoResult, Servo>()
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextEncrypter()));

        CreateMap<ServoTempDto, ServoTemp>()
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextEncrypter()));

        CreateMap<ServoTempDtoResult, ServoTemp>()
            .ForMember(dest => dest.Name, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Email, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.Cpf, opt => opt.ConvertUsing(new TextEncrypter()))
            .ForMember(dest => dest.CellPhone, opt => opt.ConvertUsing(new TextEncrypter()));

        CreateMap<FormacaoDtoResult, Formacao>()
            .ReverseMap();

        CreateMap<EventoDtoResult, Evento>()
            .ReverseMap();

        CreateMap<InscricoesEventoDtoResult, PagamentoAsaas>()
            .ReverseMap();

        CreateMap<FormacoesServoDtoResult, FormacoesServo>()
            .ReverseMap();

        CreateMap<UserDtoAdd, User>()
            .ReverseMap();

        CreateMap<UserDtoResult, User>()
            .ReverseMap();

        CreateMap<UserDto, User>()
            .ReverseMap();
    }

    public class TextEncrypter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(sourceMember))
                return string.Empty;
            return Utils.Encrypt(sourceMember);
        }
    }
}
