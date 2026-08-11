using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(x => x.PaymentId);
        
        builder.HasIndex(x => x.InvoiceId);
        
        builder.Property(x => x.PaymentMethod)
            .HasConversion<string>();
        
        builder.Property(x => x.PaymentStatus)
            .HasConversion<string>();
    }
}