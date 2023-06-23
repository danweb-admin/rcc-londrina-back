using System;
namespace RccManager.Domain.Dtos.DecanatoSetor;

public class DecanatoSetorDtoResult
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; set; }
}

