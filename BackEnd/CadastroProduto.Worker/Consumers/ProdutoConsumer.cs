using CadastroProduto.CQS;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CadastroProduto.Worker
{
    public class ProdutoConsumer :
        IConsumer<ProdutoCriado>,
        IConsumer<ProdutoAlterado>,
        IConsumer<ProdutoExcluido>
    {
        ILogger<ProdutoConsumer> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string cadastroProdutoAlias = "cadastro-produto-api";

        public ProdutoConsumer(ILogger<ProdutoConsumer> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task Consume(ConsumeContext<ProdutoCriado> context)
        {
            _logger.LogInformation("Mensagem ProdutoCriado recebida, Guid:{Value}", context.Message.Guid);

            var client = _httpClientFactory.CreateClient(cadastroProdutoAlias);

            var content = JsonConvert.SerializeObject(new Produto { Estoque = context.Message.Estoque, Guid = context.Message.Guid, Nome = context.Message.Nome, Preco = context.Message.Preco });

            var retorno = await client.PostAsync("api/Produto", new StringContent(content, Encoding.UTF8, "application/json"));

            retorno.EnsureSuccessStatusCode();
        }

        public async Task Consume(ConsumeContext<ProdutoAlterado> context)
        {
            _logger.LogInformation("Mensagem ProdutoAlterado recebida, Guid:{Value}", context.Message.Guid);

            var client = _httpClientFactory.CreateClient(cadastroProdutoAlias);

            var content = JsonConvert.SerializeObject(new Produto { Estoque = context.Message.Estoque, Guid = context.Message.Guid, Nome = context.Message.Nome, Preco = context.Message.Preco });

            var retorno = await client.PutAsync("api/Produto", new StringContent(content, Encoding.UTF8, "application/json"));

            retorno.EnsureSuccessStatusCode();
        }

        public async Task Consume(ConsumeContext<ProdutoExcluido> context)
        {
            _logger.LogInformation("Mensagem ProdutoExcluido recebida, Guid:{Value}", context.Message.Guid);

            var client = _httpClientFactory.CreateClient(cadastroProdutoAlias);

            var retorno = await client.DeleteAsync($"api/Produto/{context.Message.Guid}");

            retorno.EnsureSuccessStatusCode();
        }
    }
}
