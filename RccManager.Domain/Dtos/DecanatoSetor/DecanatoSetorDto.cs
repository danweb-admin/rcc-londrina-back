using System;
using System.ComponentModel.DataAnnotations;

namespace RccManager.Domain.Dtos.DecanatoSetor
{
	public class DecanatoSetorDto
	{
        public bool Active { get; set; }


        [Required(ErrorMessage = "O campo nome é obrigatório.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage =
            "O campo nome deve ter no mínimo 5 e no máximo 50 caracteres.")]
        public string Name { get; set; }
    }
}

