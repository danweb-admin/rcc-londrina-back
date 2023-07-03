using System;
namespace RccManager.Domain.Entities;

public class ParoquiaCapela : BaseEntity
{
    public string Address { get; set; }
    public string Neighborhood { get; set; }
    public string Name { get; set; }
    public Guid DecanatoId { get; set; }
    public DecanatoSetor DecanatoSetor { get; set; }
    public string City { get; set; }
}

