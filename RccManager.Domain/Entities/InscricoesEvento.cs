using System;
namespace RccManager.Domain.Entities
{
	public class InscricoesEvento : BaseEntity
	{
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public Guid? GrupoOracaoId { get; set; }
        public Guid EventId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Value { get; set; }
        public string Registered { get; set; }
        public string Status { get; set; }

        public GrupoOracao GrupoOracao { get; set; }
        public Evento Evento { get; set; }
    }
}

