using System;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
	public interface IPagSeguroService
	{
        Task<PagSeguroResponse> GerarLinkPagamentoAsync(InscricaoDto inscricao);
        Task<PagSeguroResponse> GerarPagamentoCartaoAsync(InscricaoDto inscricao);

    }
}

