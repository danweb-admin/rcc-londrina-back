using System;
using FluentValidation;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Entities;

namespace RccManager.Service.Validators.DecanatoSetor;

public class DecanatoSetorValidator : AbstractValidator<DecanatoSetorDto>
{
    public DecanatoSetorValidator()
    {
        RuleFor(decanatoSetor => decanatoSetor.Name).NotEmpty().WithMessage("O nome é obrigatório.");
        RuleFor(decanatoSetor => decanatoSetor.Active).NotNull().WithMessage("O campo 'ativo' é obrigatório.");
    }
}

