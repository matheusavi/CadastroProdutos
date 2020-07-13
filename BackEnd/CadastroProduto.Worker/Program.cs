using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroProduto.CQS;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                        serviceCollectionConfigurator.AddConsumer<EventConsumer>();

                        serviceCollectionConfigurator.AddBus(serviceProvider =>
                            Bus.Factory.CreateUsingRabbitMq(busConfigurator =>
                            {
                                busConfigurator.ReceiveEndpoint("event-listener", endpointConfigurator =>
                                    {
                                        endpointConfigurator.ConfigureConsumer<EventConsumer>(serviceProvider);
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

        class EventConsumer :
        IConsumer<ProdutoCriado>,
            IConsumer<ProdutoAlterado>,
            IConsumer<ProdutoExcluido>
        {
            ILogger<EventConsumer> _logger;

            public EventConsumer(ILogger<EventConsumer> logger)
            {
                _logger = logger;
            }

            public async Task Consume(ConsumeContext<ProdutoCriado> context)
            {
                _logger.LogInformation("Value: {Value}", context.Message.Guid);
            }

            public async Task Consume(ConsumeContext<ProdutoAlterado> context)
            {
                _logger.LogInformation("Value: {Value}", context.Message.Guid);
            }

            public async Task Consume(ConsumeContext<ProdutoExcluido> context)
            {
                _logger.LogInformation("Value: {Value}", context.Message.Guid);
            }
        }
    }
}
