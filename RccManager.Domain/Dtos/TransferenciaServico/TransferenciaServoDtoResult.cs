using System;
using RccManager.Domain.Dtos.GrupoOracao;
using RccManager.Domain.Dtos.Servo;

namespace RccManager.Domain.Dtos.TransferenciaServico
{
    public class TransferenciaServoDtoResult
    {
        public Guid ServoId { get; set; }
        public Guid GrupoOracaoId { get; set; }
        public Guid GrupoOracaoAntigoId { get; set; }
        public string Efetuado { get; set; }
        public string Solicitado { get; set; }
        public ServoDto Servo { get; set; }
        public GrupoOracaoDto GrupoOracao { get; set; }
        public GrupoOracaoDto GrupoOracaoAntigo { get; set; }
    }
}

