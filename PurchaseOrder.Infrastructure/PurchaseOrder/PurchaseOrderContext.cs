using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Domain.Entities;

namespace PurchaseOrder.Infrastructure.PurchaseOrder
{
    public class PurchaseOrderContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<PuchaseOrder> PurchaseOrders => Set<PuchaseOrder>();
        public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
        public DbSet<State> States => Set<State>();
        public PurchaseOrderContext(DbContextOptions<PurchaseOrderContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PurchaseOrderContext).Assembly);
        }
    }
}
