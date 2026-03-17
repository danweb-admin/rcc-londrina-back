namespace RccManager.Domain.Entities;

public class ServoTemp : BaseEntity
{
    public string Name { get; set; }
    public string Birthday { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string CellPhone { get; set; }
    public string MainMinistry { get; set; }
    public string SecondaryMinistry { get; set; }
    public string NamePlain { get; set; }
    public string CpfPlain { get; set; }
    public string EmailPlain { get; set; }
    public string CellphonePlain { get; set; }
    public bool Checked { get; set; }
    public Guid GrupoOracaoId { get; set; }
    public GrupoOracao GrupoOracao { get; set; }
}

