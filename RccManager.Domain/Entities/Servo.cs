using System;
namespace RccManager.Domain.Entities;

public class Servo : BaseEntity
{
	public string Name { get; set; }
	public DateTime Birthday { get; set; }
	public string Cpf { get; set; }
	public string Email { get; set; }
	public string CellPhone { get; set; }
	public string MainMinistry { get; set; }
	public string SecondaryMinistry { get; set; }
	public string NamePlain { get; set; }
    public string CpfPlain { get; set; }
    public string EmailPlain { get; set; }
    public string CellphonePlain { get; set; }
	public Guid GrupoOracaoId { get; set; }
	public GrupoOracao GrupoOracao { get; set; }
}

