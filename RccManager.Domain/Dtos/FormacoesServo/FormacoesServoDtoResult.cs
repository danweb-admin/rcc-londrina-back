using System;
using RccManager.Domain.Dtos.Formacao;
using RccManager.Domain.Dtos.Servo;
using RccManager.Domain.Dtos.Users;

namespace RccManager.Domain.Dtos.FormacoesServo
{
	public class FormacoesServoDtoResult
	{
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid ServoId { get; set; }
        public ServoDtoResult Servo { get; set; }
        public Guid FormacaoId { get; set; }
        public FormacaoDtoResult Formacao { get; set; }
        public Guid UsuarioId { get; set; }
        public UserDtoResult Usuario { get; set; }
        public DateTime CertificateDate { get; set; }
        public bool Active { get; set; }
    }
}

