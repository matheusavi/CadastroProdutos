using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Worker
{
    public class Produto
    {
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public long Estoque { get; set; }
    }
}
