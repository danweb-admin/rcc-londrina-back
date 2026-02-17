using System;
namespace RccManager.Domain.Entities
{
    public class InscricaoCampoValores
    {
        public Guid Id { get; set; }
        public Guid InscricaoId { get; set; }
        public Guid EventoCampoId { get; set; }
        public string Valor { get; set; }
    }
}

