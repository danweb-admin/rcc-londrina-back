using System;
using System.ComponentModel.DataAnnotations;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
	public interface IEventoService
	{
        Task<IEnumerable<EventoDtoResult>> GetAll();
        Task<IEnumerable<EventoDtoResult>> GetEventsByEmail(string email);
        Task<IEnumerable<EventoDtoResult>> GetAllHome();
        Task<EventoDto> GetSlug(string slug);
        Task<EventoDto> GetById(Guid id);
        Task<HttpResponse> Create(EventoDto dto);
        Task<HttpResponse> Update(EventoDto dto, Guid id);
        Task<decimal> LoteInscricao(Guid id);
        Task<InscricaoDto> Inscricao(InscricaoDto inscricao);
        Task<ValidationResult> ReenvioComprovante(string codigoInscricao, string email);
        Task<ValidationResult> IsentarInscricao(string codigoInscricao);
        Task<string> VerificaStatus(string codigoInscricao);
        Task<IEnumerable<InscricaoDto>> GetAllInscricoesByEvento(Guid eventoId);
        Task<HttpResponse> FazerCheckin(string codigoInscricao);
        Task<ValidationResult> EventosWebhook(string response);


    }
}

