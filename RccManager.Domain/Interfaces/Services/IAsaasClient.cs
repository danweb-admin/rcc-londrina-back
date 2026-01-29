using System;
using RccManager.Domain.Responses;

namespace RccManager.Domain.Interfaces.Services
{
  public interface IAsaasClient
  {
    Task<AsaasCustomerResponse> CriarClienteAsync(AsaasCustomerRequest request);
    Task<AsaasCustomerResponse> BuscarClientePorCpfAsync(string cpfCnpj);
    Task<AsaasPaymentConfirmedResponse> ConfirmarRecebimentoDinheiroAsyn(AsaasConfirmarRecebimentoDinheiroRequest req);

    Task<AsaasPaymentCreatedResponse> CreatePaymentAsync(AsaasCreatePaymentRequest req);
    Task<AsaasPixResponse> GetPixQrCodeAsync(string paymentId);
    Task SimulatePaymentAsync(string paymentId); // sandbox: optional
    Task<AsaasPaymentGetResponse> GetPaymentAsync(string paymentId);
    Task<AsaasPaymentResponse> CriarCobrancaCartaoAsync(AsaasCreatePaymentRequest request);
    Task<AsaasCardPaymentResult> PagarComCartaoAsync(string paymentId, CartaoDto cartao);

  }
}

