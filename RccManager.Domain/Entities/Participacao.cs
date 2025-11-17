using System;
namespace RccManager.Domain.Entities
{
    public class Participacao
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public string Descricao { get; set; }
        public Guid EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}

