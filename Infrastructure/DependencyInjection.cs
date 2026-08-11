using Application.Abstractions;
using Domain.Respository;
using Infrastructure.Consumers;
using Infrastructure.Persistance;
using Infrastructure.Producer;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IEventPublisher, EventPublisher>();
        
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddMassTransit(busConfiguration =>
        {
            busConfiguration.SetKebabCaseEndpointNameFormatter();

            busConfiguration.AddConsumer<InvoiceCreatedConsumer>();
            
            busConfiguration.AddEntityFrameworkOutbox<AppDbContext>(options =>
            {
                options.UseBusOutbox();
                options.UsePostgres();
                options.DisableInboxCleanupService();
            });
            
            busConfiguration.UsingRabbitMq((context, cfg) =>
            {
                var host = configuration["RabbitMQ:Host"];
                var port = configuration["RabbitMQ:Port"];
                
                cfg.Host(host, 
                    ushort.Parse(port!),
                    "/",
                    h =>
                    {
                        h.Username(configuration["RabbitMQ:Username"]!);
                        h.Password(configuration["RabbitMQ:Password"]!);
                    });
        
                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;    
    }
}