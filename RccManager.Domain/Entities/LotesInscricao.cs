using System;
namespace RccManager.Domain.Entities
{
    public class LoteInscricao
    {
        public Guid Id { get; set; }
        public Guid EventoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public decimal Valor { get; set; }
        public virtual Evento Evento { get; set; } = null!;
    }
}

