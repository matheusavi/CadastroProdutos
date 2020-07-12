using MediatR;

namespace CadastroProduto.CQS
{
    public class AtualizarProdutoCommand : IRequest<bool>, IProdutoCommand
    {
        public long Id { get; private set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        /// <example>Smartphone</example>
        public string Nome { get; set; }


        /// <summary>
        /// Preço do produto
        /// </summary>
        /// <example>25.00</example>
        public decimal Preco { get; set; }

        /// <summary>
        /// Estoque do produto
        /// </summary>
        /// <example>40</example>
        public long Estoque { get; set; }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}
