using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Configuration;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PuchaseOrder>
{
    public void Configure(EntityTypeBuilder<PuchaseOrder> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.Customer)
            .WithMany(e => e.PurchaseOrders)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.DeliveryAddress);

        builder.HasOne(e => e.State)
            .WithMany(e => e.PurchaseOrders)
            .HasForeignKey(e => e.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.PriorityId);

        builder.Property(e => e.CreatedDate);

        builder.Property(e => e.ModifiedDate)
            .IsRequired(false);

        builder.Property(e => e.TotalAmount);


    }
}
