using System;
namespace RccManager.Domain.Entities
{
    public class Sobre
    {
        public Guid Id { get; set; }
        public string Conteudo { get; set; }
        public Guid EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}

