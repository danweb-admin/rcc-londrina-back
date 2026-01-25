using System;
namespace RccManager.Domain.Dtos.TransferenciaServico
{
    public class TransferenciaServoDto
    {
        public Guid ServoId { get; set; }
        public Guid GrupoOracaoId { get; set; }
        public Guid GrupoOracaoAntigoId { get; set; }
        public string Efetuado { get; set; }
        public string Solicitado { get; set; }
    }
}

