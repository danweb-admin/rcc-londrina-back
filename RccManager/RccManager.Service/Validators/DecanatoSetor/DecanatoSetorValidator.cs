using System;
using FluentValidation;
using RccManager.Domain.Dtos.DecanatoSetor;
using RccManager.Domain.Entities;

namespace RccManager.Service.Validators.DecanatoSetor;

public class DecanatoSetorValidator : AbstractValidator<DecanatoSetorDto>
{
	public DecanatoSetorValidator()
	{
		RuleFor(x => x.Name)
			.NotNull().WithMessage("Campo name não poder ser nulo.")
                .NotEmpty().WithMessage("Campo name não poder ser em branco.")
			.Length(5,50).WithMessage("Campo name não pode conter mais de 50 caracteres.");

		RuleFor(x => x.Active)
			.NotNull().WithMessage("Campo active não pode ser nulo.");

	}
}

