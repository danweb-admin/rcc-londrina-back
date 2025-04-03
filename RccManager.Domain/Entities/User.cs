
namespace RccManager.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string NickName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime? ConfirmationDate { get; set; }
    public Guid? DecanatoSetorId { get; set; }
    public Guid? GrupoOracaoId { get; set; }
    public GrupoOracao GrupoOracao { get; set; }
    public DecanatoSetor DecanatoSetor { get; set; }

}

