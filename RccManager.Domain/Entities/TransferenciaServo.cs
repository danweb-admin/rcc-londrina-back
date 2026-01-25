using System;
namespace RccManager.Domain.Entities
{
    public class TransferenciaServo : BaseEntity
    {
        public Guid ServoId { get; set; }
        public Guid GrupoOracaoId { get; set; }
        public Guid GrupoOracaoAntigoId { get; set; }
        public string Efetuado { get; set; }
        public string Solicitado { get; set; }
        public Servo Servo { get; set; }
        public GrupoOracao GrupoOracao { get; set; }
        public GrupoOracao GrupoOracaoAntigo { get; set; }

    }
}

