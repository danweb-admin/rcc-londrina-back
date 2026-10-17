using System;
namespace RccManager.Domain.Entities
{
    
    public class FormularioCadastroServo : BaseEntity
    {

        public Guid GrupoOracaoId { get; set; }

        public string Token { get; set; }

        public virtual GrupoOracao GrupoOracao { get; set; }
    }

}

