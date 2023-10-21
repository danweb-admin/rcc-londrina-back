﻿using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Dtos.GrupoOracao;

namespace RccManager.Domain.Dtos.Users;

public class UserDtoResult
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string NickName { get; set; }
    public Guid? DecanatoSetorId { get; set; }
    public Guid? GrupoOracaoId { get; set; }
    public DecanatoSetorDtoResult DecanatoSetor { get; set; }
    public GrupoOracaoDtoResult GrupoOracao { get; set; }

}

