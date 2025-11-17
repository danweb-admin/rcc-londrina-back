using System;
namespace RccManager.Domain.Entities
{
    public class Programacao
    {
        public Guid Id { get; set; }
        public string Dia { get; set; }
        public string Descricao { get; set; }
        public Guid EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}

