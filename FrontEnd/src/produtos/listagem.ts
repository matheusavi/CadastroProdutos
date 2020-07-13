import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-dependency-injection';
import { ProdutoService } from 'services/ProdutoService';
import { Produto } from 'models/produto';

@autoinject
export class Listagem {
    produtoService: ProdutoService;
    produtos: Array<Produto>;
    router: Router;

    constructor(
        produtoService: ProdutoService,
        router: Router
    ) {
        this.produtoService = produtoService;
        this.router = router;

        this.produtoService.getAll().then((response) => {
            if (!response.error)
                this.produtos = response.response;
        });
    }

    async excluirProduto(id: number) {
        let response = await this.produtoService.delete(id);
        if (!response.error) {
            let index = this.produtos.findIndex(x => x.id == id);
            if (index > -1) {
                this.produtos.splice(index, 1);
            }
        } else {
            alert('Ocorreu um erro ao excluir o produto');
        }
    }

    editarProduto(id: number) {
        this.router.navigateToRoute("cadastrar", { id: id });
    }
}


