using System;
using RccManager.Domain.Dtos.InscricoesEvento;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
	public interface IInscricoesEventoService
	{
        Task<IEnumerable<InscricoesEventoDtoResult>> GetAll(Guid eventoId);

        Task<HttpResponse> Create(InscricoesEventoDto inscricao);

        Task<HttpResponse> Update(InscricoesEventoDto inscricao, Guid id);

        Task<HttpResponse> CreateInscricaoByCpf(string cpf, Guid eventId);
    }
}

