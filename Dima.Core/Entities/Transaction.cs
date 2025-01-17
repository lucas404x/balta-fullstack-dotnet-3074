using Dima.Core.Enums;
using Dima.Core.Extensions;
using Dima.Core.Validators;
using FluentValidation;

namespace Dima.Core.Entities;

public sealed class Transaction : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public DateTime? PaidOrReceivedAt { get; set; }
    public ETransactionType Type { get; set; } = ETransactionType.Withdraw;
    public decimal Amount { get; set; }
    public long SeqCategory { get; set; }
    public Category Category { get; set; } = null!;

    public override List<string> Validate() 
        => new TransactionValidator().Validate(this).GetErrorMessages();
}