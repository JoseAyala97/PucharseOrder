using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Infrastructure.PurchaseOrder.Configuration;

public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name);

        builder.Property(e => e.Description);

        builder.HasData(
            new State { Id = 1, Name = "Registrado", Description = "Pedido Creado" },
            new State { Id = 2, Name = "Confirmado", Description = "Pedido Confirmado" },
            new State { Id = 3, Name = "Anulado", Description = "Pedido Anulado" }
        );
    }
}
