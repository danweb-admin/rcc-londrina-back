using System;
namespace RccManager.Domain.Responses
{
  public record AsaasCreatePaymentRequest(string customer, string billingType, decimal value, string description, DateTime dueDate);

  public class AsaasCustomerRequest
  {
      public string Name { get; set; }
      public string CpfCnpj { get; set; }
      public string Email { get; set; }
      public string MobilePhone { get; set; }
      public bool NotificationDisabled { get; set; }
  }

  public class AsaasCustomerListResponse
  {
      public string Object { get; set; }
      public bool HasMore { get; set; }
      public int TotalCount { get; set; }
      public List<AsaasCustomerResponse> Data { get; set; }
  }

  public class AsaasCustomerResponse
  {
      public string Object { get; set; }
      public string Id { get; set; }
      public string Name { get; set; }
      public string CpfCnpj { get; set; }
      public string Email { get; set; }
      public bool Deleted { get; set; }
  }

  public class AsaasPaymentCreatedResponse
  {
      public string id { get; set; }
      public string status { get; set; }
      public decimal value { get; set; }
  }

  public class AsaasPixResponse
  {
      public string encodedImage { get; set; }
      public string payload { get; set; }
      public string expirationDate { get; set; }
  }

  public class AsaasPaymentGetResponse
  {
      public string id { get; set; }
      public string status { get; set; }
      public PixDetails pixTransaction { get; set; }
      public decimal value { get; set; }
      public string billingType { get; set; }
  }

  public class PixDetails {
      public string payload { get; set; }
      public string qrCodeImage { get; set; }
  }

  public class AsaasWebhookPayload
  {
      public string @event { get; set; }
      public DateTime? dateCreated { get; set; }
      public AsaasWebhookPayment payment { get; set; }
  }

  public class AsaasWebhookPayment {
      public string id { get; set; }
      public string status { get; set; }
      public decimal value { get; set; }
      public string description { get; set; }
  }

  public class PagamentoRequest
  {
      // Obrigatórios
      public string Customer { get; set; } = null!;          // ID do cliente no Asaas (cus_xxx)
      public string BillingType { get; set; } = null!;       // PIX | CREDIT_CARD
      public decimal Value { get; set; }                     // Valor da cobrança
      public DateTime DueDate { get; set; }                  // Data de vencimento

      // Identificação no seu sistema
      public string? ExternalReference { get; set; }         // Ex: INS-123456
      public string? Description { get; set; }               // Descrição da cobrança
      public string? GroupName { get; set; }                 // Ex: congresso-jovem-2025

      // Parcelamento (Cartão)
      public int? InstallmentCount { get; set; }             // Nº de parcelas
      public decimal? InstallmentValue { get; set; }         // Valor da parcela

      // Cartão de crédito (só quando BillingType = CREDIT_CARD)
      public CreditCardRequest? CreditCard { get; set; }
      public CreditCardHolderInfoRequest? CreditCardHolderInfo { get; set; }
  }

  public class CreditCardRequest
  {
      public string HolderName { get; set; } = null!;
      public string Number { get; set; } = null!;
      public string ExpiryMonth { get; set; } = null!;
      public string ExpiryYear { get; set; } = null!;
      public string Ccv { get; set; } = null!;
  }

  public class CreditCardHolderInfoRequest
  {
      public string Name { get; set; } = null!;
      public string Email { get; set; } = null!;
      public string CpfCnpj { get; set; } = null!;
      public string PostalCode { get; set; } = null!;
      public string AddressNumber { get; set; } = null!;
      public string Phone { get; set; } = null!;
  }

  public class AsaasPaymentResponse
  {
      public string Object { get; set; }
      public string Id { get; set; }
      public DateTime DateCreated { get; set; }

      public string Customer { get; set; }

      public string BillingType { get; set; } // PIX | CREDIT_CARD | BOLETO
      public decimal Value { get; set; }
      public decimal NetValue { get; set; }

      public string Status { get; set; } // PENDING | RECEIVED | CONFIRMED | OVERDUE | REFUNDED
      public DateTime DueDate { get; set; }
      public DateTime? PaymentDate { get; set; }
      public DateTime? ClientPaymentDate { get; set; }

      public string Description { get; set; }
      public string ExternalReference { get; set; }

      public string InvoiceUrl { get; set; }
      public string TransactionReceiptUrl { get; set; }
  }

  public class CartaoDto
  {
      // Dados do cartão
      public string Numero { get; set; }          // 4111111111111111
      public string Nome { get; set; }            // Nome impresso no cartão
      public string ValidadeMes { get; set; }     // MM
      public string ValidadeAno { get; set; }     // YYYY
      public string Cvv { get; set; }             // 123

      // Dados do portador (obrigatórios pelo Asaas)
      public string Email { get; set; }
      public string Cpf { get; set; }
      public string Cep { get; set; }
      public string NumeroEndereco { get; set; }
      public string Telefone { get; set; }
      public CreditCardHolderInfoRequest CreditCardHolderInfo { get; set; }
  }

  public class AsaasCardPaymentResult
  {
      public string Object { get; set; }           // payment
      public string Id { get; set; }               // pay_xxxxx
      public string Status { get; set; }           // CONFIRMED, RECEIVED, DECLINED
      public decimal Value { get; set; }
      public DateTime DateCreated { get; set; }
      public string Description { get; set; }
      public string BillingType { get; set; }      // CREDIT_CARD
      public string Customer { get; set; }

      public CreditCardInfo CreditCard { get; set; }
      public CreditCardTransactionInfo CreditCardTransaction { get; set; }
  }

  public class CreditCardInfo
  {
      public string CreditCardBrand { get; set; }  // VISA, MASTERCARD etc
      public string LastFourDigits { get; set; }   // 1234
  }

  public class CreditCardTransactionInfo
  {
      public string Status { get; set; }           // CONFIRMED / DECLINED
      public string AuthorizationCode { get; set; }
      public string Nsu { get; set; }
  }


}

