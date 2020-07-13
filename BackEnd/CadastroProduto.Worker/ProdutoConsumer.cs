using CadastroProduto.CQS;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CadastroProduto.Worker
{
    public class ProdutoConsumer :
        IConsumer<ProdutoCriado>,
        IConsumer<ProdutoAlterado>,
        IConsumer<ProdutoExcluido>
    {
        ILogger<ProdutoConsumer> _logger;

        public ProdutoConsumer(ILogger<ProdutoConsumer> logger)
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
