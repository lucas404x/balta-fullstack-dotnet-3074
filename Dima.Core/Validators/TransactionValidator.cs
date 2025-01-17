using Dima.Core.Entities;
using FluentValidation;

namespace Dima.Core.Validators;

public sealed class TransactionValidator : AbstractValidator<Transaction>
{
    public TransactionValidator()
    {
        Include(new BaseEntityValidator());
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("O título não pode estar vazio.")
            .MaximumLength(80)
            .WithMessage("O título deve conter até 80 caracteres.");
        RuleFor(x => x.PaidOrReceivedAt)
            .NotNull()
            .WithMessage("A data de pagamento ou recebimento n�o pode estar vazia.");
        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("O tipo de transação não é inválido.");
        RuleFor(x => x.Category)
            .SetValidator(new CategoryValidator());
    }
}