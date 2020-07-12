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
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<EventConsumer>();

                        //x.AddBus(serviceProvider =>
                        //    Bus.Factory.CreateUsingRabbitMq(j =>
                        //    {
                        //        j.ReceiveEndpoint("event-listener", e =>
                        //            {
                        //                e.ConfigureConsumer<EventConsumer>(serviceProvider);
                        //            });
                        //        j.Host("localhost", y =>
                        //        {
                        //            y.Username("test");
                        //            y.Password("test");
                        //        });
                        //    })
                        //);

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.ReceiveEndpoint("event-listener", e =>
                            {
                                e.ConfigureConsumer<EventConsumer>(context);
                            });
                        });
                    });

                    services.AddMassTransitHostedService();
                });

        class EventConsumer :
        IConsumer<ProdutoCriado>
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
        }
    }
}
