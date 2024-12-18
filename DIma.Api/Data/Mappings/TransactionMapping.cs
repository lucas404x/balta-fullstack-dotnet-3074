using Dima.Core.Entities;
using Dima.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class TransactionMapping : BaseEntityConfigurations<Transaction>
{
    public override void Configure(EntityTypeBuilder<Transaction> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Transactions");

        builder
            .HasOne(x => x.Category)
            .WithOne()
            .HasForeignKey<Transaction>(x => x.SeqCategory);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(100);
        
        builder.Property(x => x.PaidOrReceivedAt)
            .HasColumnType("datetime");
        
        builder.Property(x => x.Type)
            .IsRequired()
            .HasColumnType("tinyint")
            .HasDefaultValue(ETransactionType.Withdraw);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("money")
            .HasDefaultValue(0);
        
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);
    }
}
