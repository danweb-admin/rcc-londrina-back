using System;
namespace RccManager.Domain.Dtos.ServoTemp;

public class ServoTempDto
{
    public bool Active { get; set; }
    public string Name { get; set; }
    public string Birthday1 { get; set; }
    public DateTime Birthday { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string CellPhone { get; set; }
    public string MainMinistry { get; set; }
    public string SecondaryMinistry { get; set; }
    public bool Checked { get; set; }
    public string GrupoOracaoName { get; set; }
    public string ParoquiaCapelaName { get; set; }
    public Guid GrupoOracaoId { get; set; }
}

