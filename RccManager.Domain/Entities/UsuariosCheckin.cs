using System;
namespace RccManager.Domain.Entities
{
    public class UsuariosCheckin : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid EventoId { get; set; }
        public Evento Evento { get; set; }
    }
}

