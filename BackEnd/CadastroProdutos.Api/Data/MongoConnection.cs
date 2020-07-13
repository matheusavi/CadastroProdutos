namespace CadastroProdutos.Api
{
    public class MongoConnection
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public bool IsSSL { get; set; }
    }
}
