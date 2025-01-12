using Dima.Core.Enums;
using Dima.Core.Extensions;
using FluentValidation;

namespace Dima.Core.Entities;

public sealed class Transaction : BaseEntity
{
    #region Validation

    private readonly TransactionValidator _validator = new();

    internal class TransactionValidator : AbstractValidator<Transaction>
    {
        public TransactionValidator()
        {
            Include(new BaseEntityValidator());
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("O t�tulo n�o pode estar vazio.")
                .MaximumLength(80)
                .WithMessage("O t�tulo deve conter at� 80 caracteres.");
            RuleFor(x => x.PaidOrReceivedAt)
                .NotNull()
                .WithMessage("A data de pagamento ou recebimento n�o pode estar vazia.");
            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("O tipo de transa��o n�o � inv�lido.");
            RuleFor(x => x.Category)
                .SetValidator(new Category.CategoryValidator());
        }
    }

    #endregion

    public string Title { get; set; } = string.Empty;
    public DateTime? PaidOrReceivedAt { get; set; }
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    public decimal Amount { get; set; }
    public long SeqCategory { get; set; }
    public Category Category { get; set; } = null!;

    public override List<string> Validate() 
        => _validator.Validate(this).GetErrorMessages();
}