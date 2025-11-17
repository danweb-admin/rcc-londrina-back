using System;
using RccManager.Domain.Dtos.Evento;
using RccManager.Domain.Dtos.GrupoOracao;

namespace RccManager.Domain.Dtos.InscricoesEvento
{
	public class InscricoesEventoDtoResult
	{
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
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

        public GrupoOracaoDto GrupoOracao { get; set; }
        public EventoDto Evento { get; set; }
    }
}

