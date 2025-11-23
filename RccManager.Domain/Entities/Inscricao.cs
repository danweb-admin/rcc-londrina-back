using System;
using System.Text.Json.Serialization;

namespace RccManager.Domain.Entities
{
    public class Inscricao : BaseEntity
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string CodigoInscricao { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string GrupoOracao { get; set; }
        public string Decanato { get; set; }
        public string TipoPagamento { get; set; }
        public string Status { get; set; }
        public decimal ValorInscricao { get; set; }
        public DateTime? DataPagamento { get; set; }
        public Guid EventoId { get; set; }
        public Guid? GrupoOracaoId { get; set; }
        public Guid? DecanatoId { get; set; }
        public Guid? ServoId { get; set; }
        public string LinkQrCodePNG { get; set; }
        public string LinkQrCodeBase64 { get; set; }
        public string QRCodeCopiaCola { get; set; }
        [JsonIgnore]
        public virtual Evento Evento { get; set; }
    }
}

