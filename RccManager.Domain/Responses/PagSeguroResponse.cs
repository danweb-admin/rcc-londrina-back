using System;
namespace RccManager.Domain.Responses
{
    public class PagSeguroResponse
    {
        public string Id { get; set; }
        public string Reference_Id { get; set; }
        public DateTime Created_At { get; set; }

        public Customer Customer { get; set; }
        public List<Item> Items { get; set; }
        public List<QrCode> Qr_Codes { get; set; }
        public List<Charge> Charges { get; set; }

        public List<string> Notification_Urls { get; set; }
        public List<Link> Links { get; set; }
    }

    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Tax_Id { get; set; }
        public List<Phone> Phones { get; set; }
    }

    public class Phone
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string Number { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Unit_Amount { get; set; }
    }

    

    public class QrCode
    {
        public string Id { get; set; }
        public DateTime Expiration_Date { get; set; }
        public Amount Amount { get; set; }
        public string Text { get; set; }
        public List<string> Arrangements { get; set; }
        public List<Link> Links { get; set; }
    }

    public class Charge
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public Amount Amount { get; set; }
        public PaymentMethod Payment_Method { get; set; }
    }

    public class PaymentMethod
    {
        public string Type { get; set; }                 // "CREDIT_CARD", "PIX", etc.
        public int Installments { get; set; }            // Número de parcelas
        public bool Capture { get; set; }                // Se deve capturar automaticamente
        public Card Card { get; set; }                   // Dados do cartão
        public Holder Holder { get; set; }               // Dados do titular
    }

    public class Card
    {
        public string Number { get; set; }               // Número do cartão
        public string Exp_Month { get; set; }            // Mês (MM)
        public string Exp_Year { get; set; }             // Ano (YYYY)
        public string Security_Code { get; set; }        // CVV
        public bool Store { get; set; }                  // Armazenar cartão?
    }

    public class Holder
    {
        public string Name { get; set; }                 // Nome do titular
        public string Tax_Id { get; set; }               // CPF
    }

    public class Amount
    {
        public int Value { get; set; }
    }

    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Media { get; set; }
        public string Type { get; set; }
    }

    public class PagSeguroErrorResponse
    {
        public List<PagSeguroErrorMessage> Error_Messages { get; set; }
    }

    public class PagSeguroErrorMessage
    {
        public string Code { get; set; }
        public string Error { get; set; }
        public string Description { get; set; }
        public string Parameter_Name { get; set; }
    }

}

