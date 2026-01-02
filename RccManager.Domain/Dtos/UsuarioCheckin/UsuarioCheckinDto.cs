using System;
namespace RccManager.Domain.Dtos.UsuarioCheckin
{
    public class UsuarioCheckinDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid EventoId { get; set; }
    }
}

