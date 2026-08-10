using Infrastructure.Consumers;
using Infrastructure.Persistance;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        Console.WriteLine("Startinng masstransit configuration...");

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
                Console.WriteLine($"Configuring RabbitMQ at {host}:{port}");
                
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
                
                Console.WriteLine("MassTransit endpoints configured.");
            });
        });

        return services;    
    }
}