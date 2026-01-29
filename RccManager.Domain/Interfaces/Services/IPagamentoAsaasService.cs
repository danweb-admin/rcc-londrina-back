using System;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Entities;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
  public interface IPagamentoAsaasService
  {
    Task<PagamentosAsaas> CreatePixAsync(Inscricao inscricao, decimal value, string description);
    Task<AsaasPaymentConfirmedResponse> ConfirmarRecebimentoDinheiro(string id, decimal value, DateTime paymentDate);
    Task<AsaasPaymentResponse> CreateCartaoCreditoAsync(InscricaoDto inscricao, decimal value, string description);
    Task<PagamentosAsaas> GetByAsaasIdAsync(string asaasId);
    Task HandleWebhookAsync(AsaasWebhookPayload payload);
  }
}

