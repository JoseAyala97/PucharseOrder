using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name);

        builder.Property(e => e.Code);

        builder.Property(e => e.Price);

        builder.HasData(
            new Product { Id = 1, Code = "P-001", Name = "Teclado", Price = 120.00m },
            new Product { Id = 2, Code = "P-002", Name = "Mouse", Price = 60.00m },
            new Product { Id = 3, Code = "P-003", Name = "Monitor 24", Price = 950.00m }
        );  
    }
}
