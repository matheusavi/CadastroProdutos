namespace CadastroProduto.CQS
{
    public class ProdutoDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public long Estoque { get; set; }
    }
}
