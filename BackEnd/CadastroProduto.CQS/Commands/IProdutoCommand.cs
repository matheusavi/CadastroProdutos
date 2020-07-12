namespace CadastroProduto.CQS
{
    public interface IProdutoCommand
    {
        string Nome { get; }
        decimal Preco { get; }
        long Estoque { get; }
    }
}