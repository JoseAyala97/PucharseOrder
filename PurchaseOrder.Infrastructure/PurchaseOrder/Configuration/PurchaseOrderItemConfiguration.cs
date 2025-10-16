using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Configuration;

public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.PucharseOrder)
            .WithMany(e => e.PurchaseOrderItems)
            .HasForeignKey(e => e.PurchaseOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Product)
            .WithMany(e => e.PurchaseOrderItems)
            .HasForeignKey(e => e.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.UnitPrice);

        builder.Property(e => e.Quantity);

        builder.Property(e => e.LineTotal);
    }
}
