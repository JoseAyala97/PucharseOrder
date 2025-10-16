using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name);

        builder.Property(e => e.Address);

        builder.Property(e => e.Identification);

        builder.HasData(
            new Customer { Id = 1, Name = "Acme S.A.", Address = "Calle 10 # 20-30", Identification = "900123456" },
            new Customer { Id = 2, Name = "Globant", Address = "Av. 26 # 85-12", Identification = "800987654" },
            new Customer { Id = 3, Name = "SkillNet", Address = "Calle 34 #", Identification = "10191000" }
        );
    }
}
