using System;
using RccManager.Domain.Dtos.Evento;

namespace RccManager.Domain.Dtos.UsuarioCheckin
{
    public class UsuarioCheckinDtoResult
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid EventoId { get; set; }
        public EventoDto Evento { get; set; }
    }
}

