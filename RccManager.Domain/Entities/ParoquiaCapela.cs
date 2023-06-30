using System;
namespace RccManager.Domain.Entities;

public class ParoquiaCapela : BaseEntity
{
    public string Endereco { get; set; }
    public string Bairro { get; set; }
    public string Name { get; set; }
    public Guid DecanatoId { get; set; }
    public DecanatoSetor DecanatoSetor { get; set; }
    public string Cidade { get; set; }
}

