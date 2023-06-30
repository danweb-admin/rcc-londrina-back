using System;
namespace RccManager.Domain.Dtos.ParoquiaCapela
{
	public class ParoquiaCapelaDtoResult
	{
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Active { get; set; }
        public string Endereco { get; set; }
        public string Bairro { get; set; }
        public int DecanatoId { get; set; }
        public string Cidade { get; set; }
    }
}

