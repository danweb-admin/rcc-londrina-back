using System;
namespace RccManager.Domain.Entities
{
	public class FormacoesServo : BaseEntity
    {
		public Guid ServoId { get; set; }
		public Guid FormacaoId { get; set; }
		public Guid UsuarioId { get; set; }
		public DateTime CertificateDate { get; set; }
		public Servo Servo { get; set; }
		public Formacao Formacao { get; set; }
		public User Usuario { get; set; }
	}
}

