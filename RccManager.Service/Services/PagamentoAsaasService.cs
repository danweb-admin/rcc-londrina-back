using System;
using System.Net;
using Microsoft.Extensions.Logging;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Entities;
using RccManager.Domain.Interfaces.Repositories;
using RccManager.Domain.Interfaces.Services;
using RccManager.Domain.Responses;

namespace RccManager.Service.Services
{
  public class PagamentoAsaasService : IPagamentoAsaasService
{
    private readonly IAsaasClient _asaas;
    private readonly IPagamentoAsaasRepository _repo;
    private readonly ILogger<PagamentoAsaasService> _log;

    public PagamentoAsaasService(IAsaasClient asaas, IPagamentoAsaasRepository repo, ILogger<PagamentoAsaasService> log)
    {
        _asaas = asaas;
        _repo = repo;
        _log = log;
    }

        public async Task<AsaasPaymentConfirmedResponse> ConfirmarRecebimentoDinheiro(string id, decimal value, DateTime paymentDate)
        {
            var confirmacao = new AsaasConfirmarRecebimentoDinheiroRequest
            {
                id = id,
                value = value,
                paymentDate = paymentDate
            };

            var asaasRes = await _asaas.ConfirmarRecebimentoDinheiroAsyn(confirmacao);

            return asaasRes;
        }

        public async Task<AsaasPaymentResponse> CreateCartaoCreditoAsync(InscricaoDto inscricao, decimal value, string description)
    {
      // Ensure customer exists in Asaas (you can pass customer id as string or create on the fly)

        var cliente = await _asaas.CriarClienteAsync(new AsaasCustomerRequest
        {
            Name = inscricao.Nome,
            CpfCnpj = inscricao.Cpf,
            Email = inscricao.Email,
            MobilePhone = inscricao.Telefone,
            NotificationDisabled = true
        });

        var createReq = new AsaasCreatePaymentRequest(cliente.Id, "CREDIT_CARD", value, description, DateTime.Now.AddDays(1));
        var asaasRes = await _asaas.CriarCobrancaCartaoAsync(createReq);


        return asaasRes;
    }

    public async Task<PagamentosAsaas> CreatePixAsync(Inscricao inscricao, decimal value, string description)
    {
        // Ensure customer exists in Asaas (you can pass customer id as string or create on the fly)

        var cliente = await _asaas.CriarClienteAsync(new AsaasCustomerRequest
        {
            Name = inscricao.Nome,
            CpfCnpj = inscricao.Cpf,
            Email = inscricao.Email,
            MobilePhone = inscricao.Telefone,
            NotificationDisabled = true
        });

        var createReq = new AsaasCreatePaymentRequest(cliente.Id, "PIX", value, description, DateTime.Now.AddDays(1));
        var asaasRes = await _asaas.CreatePaymentAsync(createReq);

        // immediately fetch pix qrcode
        var pix = await _asaas.GetPixQrCodeAsync(asaasRes.id);

        var payment = new PagamentosAsaas
        {
            Id = Guid.NewGuid(),
            AsaasPaymentId = asaasRes.id,
            CustomerId = cliente.Id,
            Value = asaasRes.value,
            BillingType = "PIX",
            Status = asaasRes.status,
            PixPayload = pix.payload,
            PixQrBase64 = pix.encodedImage,
            CreatedAt = DateTime.UtcNow,
            InvoiceNumber = asaasRes.invoiceNumber
        };

        await _repo.AddAsync(payment);
        return payment;
    }

    public Task<AsaasPaymentResponse> CriarCobrancaCartaoAsync(PagamentoRequest request)
    {
      throw new NotImplementedException();
    }

    public Task<PagamentosAsaas> GetByAsaasIdAsync(string asaasId) => _repo.GetByAsaasIdAsync(asaasId);

    public async Task HandleWebhookAsync(AsaasWebhookPayload payload)
    {
        // payload.payment.id is asaas payment id
        var payment = await _repo.GetByAsaasIdAsync(payload.payment.id);
        if (payment == null)
        {
            _log.LogWarning("Webhook for unknown payment id {Id}", payload.payment.id);
            // you might choose to fetch from Asaas and persist
            var remote = await _asaas.GetPaymentAsync(payload.payment.id);
            payment = new PagamentosAsaas
            {
                Id = Guid.NewGuid(),
                AsaasPaymentId = remote.id,
                Value = remote.value,
                BillingType = remote.billingType,
                Status = remote.status,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(payment);
        }
        else
        {
            payment.Status = payload.payment.status ?? (payload.@event == "PAYMENT_CONFIRMED" ? "RECEIVED" : payment.Status);
            payment.UpdatedAt = DateTime.UtcNow;
            await _repo.UpdateAsync(payment);
        }

        // Business: if received, mark inscrição as paid, enviar email, etc
        if (payment.Status?.ToUpper() == "RECEIVED")
        {
            // chamar service que atualiza inscrições etc
        }
    }

    
  }
}

