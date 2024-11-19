using System;
using RccManager.Domain.Dtos.GrupoOracao;

namespace RccManager.Domain.Dtos.ServoTemp;

public class ServoTempDtoResult
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; set; }
    public string Name { get; set; }
    public string Birthday { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string CellPhone { get; set; }
    public string MainMinistry { get; set; }
    public bool Checked { get; set; }
    public string SecondaryMinistry { get; set; }
    public Guid GrupoOracaoId { get; set; }
    public GrupoOracaoDtoResult GrupoOracao { get; set; }
}

