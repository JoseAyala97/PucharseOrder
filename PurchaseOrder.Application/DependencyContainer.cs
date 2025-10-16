using Microsoft.EntityFrameworkCore;
using PurchaseOrder.Application.Features.Customer.Queries.GetAllCustomers;
using PurchaseOrder.Application.Shared;
using PurchaseOrder.Infrastructure.PurchaseOrder;
using PurchaseOrder.Infrastructure.PurchaseOrder.Interfaces;
using PurchaseOrder.Infrastructure.PurchaseOrder.Repository;

namespace PurchaseOrder.Application;

public static class DependencyContainer
{
    public static IServiceCollection AddPurchaseOrderModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PurchaseOrderDb")
            ?? throw new InvalidOperationException("Missing connection string 'PurchaseOrderDb'.");

        services.AddDbContext<PurchaseOrderContext>(opt =>
            opt.UseSqlServer(
                connectionString,
                sql => sql.MigrationsAssembly(typeof(PurchaseOrderContext).Assembly.FullName)));

        services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);
        services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssemblyContaining<GetAllCustomersQueryHandler>());

        services.AddScoped<IPurchaseOrderUnitOfWork, PurchaseOrderUnitOfWork>();
        services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();

        return services;
    }
}
