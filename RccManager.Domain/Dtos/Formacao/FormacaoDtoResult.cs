using System;
namespace RccManager.Domain.Dtos.Formacao;

public class FormacaoDtoResult
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; set; }
}

