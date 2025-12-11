using System;
namespace RccManager.Domain.Entities
{
  public class PagamentosAsaas : BaseEntity
  {
      public string AsaasPaymentId { get; set; } // pay_xxx
      public string CustomerId { get; set; }   // seu cliente (FK opcional)
      public decimal Value { get; set; }
      public string BillingType { get; set; } // PIX
      public string Status { get; set; }      // PENDING, RECEIVED, etc
      public string PixPayload { get; set; }  // copia e cola
      public string PixQrBase64 { get; set; } // imagem base64
  }
}

