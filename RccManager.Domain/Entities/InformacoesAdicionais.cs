using System;
namespace RccManager.Domain.Entities
{
    public class InformacoesAdicionais
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
        public Guid EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}

