using System;
namespace RccManager.Domain.Responses
{
    public class PagSeguroWebhook
    {
        public string Id { get; set; }
        public string Reference_Id { get; set; }
        public DateTime Created_At { get; set; }
        public CustomerWebhook Customer { get; set; }
        public List<ItemWebhook> Items { get; set; }
        public List<ChargeWebhook> Charges { get; set; }
    }

    public class CustomerWebhook
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tax_Id { get; set; }
    }

    public class ItemWebhook
    {
        public string Reference_Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Unit_Amount { get; set; }
    }

    public class ChargeWebhook
    {
        public string Id { get; set; }
        public string Reference_Id { get; set; }
        public string Status { get; set; }   // PAID, DECLINED, etc
        public DateTime Created_At { get; set; }
        public DateTime? Paid_At { get; set; }
        public PaymentMethodWebhook Payment_Method { get; set; }
        public PaymentResponseWebhook Payment_Response { get; set; }

    }

    public class PaymentMethodWebhook
    {
        public string Type { get; set; } // CREDIT_CARD, PIX, etc
        public int Installments { get; set; }
    }

    public class PaymentResponseWebhook
    {
        public string Code { get; set; } 
        public string Message { get; set; } 
        public string Reference { get; set; } 


    }

}

