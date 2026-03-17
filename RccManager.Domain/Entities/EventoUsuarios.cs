using System;
namespace RccManager.Domain.Entities
{
    public class EventoUsuarios : BaseEntity
    {
        public Guid EventoId { get; set; }
        public Guid UserId { get; set; }

        public Evento Evento { get; set; }
        public User User { get; set; }
    }
}

