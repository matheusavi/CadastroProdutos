using MassTransit;
using Microsoft.Extensions.Hosting;

namespace CadastroProduto.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(serviceCollectionConfigurator =>
                    {
                        serviceCollectionConfigurator.AddConsumer<ProdutoConsumer>();

                        serviceCollectionConfigurator.AddBus(serviceProvider =>
                            Bus.Factory.CreateUsingRabbitMq(busConfigurator =>
                            {
                                busConfigurator.ReceiveEndpoint("event-listener", endpointConfigurator =>
                                    {
                                        endpointConfigurator.ConfigureConsumer<ProdutoConsumer>(serviceProvider);
                                    });
                                busConfigurator.Host("rabbitmq", hostConfigurator =>
                                {
                                    hostConfigurator.Username("guest");
                                    hostConfigurator.Password("guest");
                                });
                            })
                        );
                    });

                    services.AddMassTransitHostedService();
                });
    }
}
