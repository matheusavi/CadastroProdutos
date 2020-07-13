using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace CadastroProdutos.Api
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(ILogger<MongoDbContext> logger, IOptions<MongoConnection> connection)
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connection.Value.ConnectionString));

                if (connection.Value.IsSSL)
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };

                var mongoClient = new MongoClient(settings);
                
                _database = mongoClient.GetDatabase(connection.Value.DatabaseName);

                var builderProduto = Builders<Produto>.IndexKeys;
                var indexModelProduto = new CreateIndexModel<Produto>(builderProduto.Ascending(x => x.Guid));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Não foi possível conectar ao servidor");
                throw ex;
            }

        }

        public IMongoCollection<Produto> Produtos
        {
            get
            {
                return _database.GetCollection<Produto>("Produto");
            }
        }
    }
}
