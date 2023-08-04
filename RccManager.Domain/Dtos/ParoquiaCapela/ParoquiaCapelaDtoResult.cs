using System;
using RccManager.Domain.Dtos.DecanatoSetor;

namespace RccManager.Domain.Dtos.ParoquiaCapela;

public class ParoquiaCapelaDtoResult
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; set; }
    public Guid DecanatoId { get; set; }
    public string Address { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public DecanatoSetorDtoResult DecanatoSetor { get; set; }
}

