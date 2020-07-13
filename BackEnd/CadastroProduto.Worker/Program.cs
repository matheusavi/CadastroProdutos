using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

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
                    services.AddHttpClient("cadastro-produto-api",
                        x =>
                        {
                            x.BaseAddress = new Uri("https://cadastroprodutos.api:443/");

                        }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                        {
                            ClientCertificateOptions = ClientCertificateOption.Manual,
                            ServerCertificateCustomValidationCallback =
                            (httpRequestMessage, cert, cetChain, policyErrors) =>
                            {
                                return true;
                            }
                        });

                    services.AddMassTransit(serviceCollectionConfigurator =>
                    {
                        serviceCollectionConfigurator.AddConsumer<ProdutoConsumer>(x => x.UseMessageRetry(r =>
                        {
                            r.Interval(10, TimeSpan.FromSeconds(1));
                        }));

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
