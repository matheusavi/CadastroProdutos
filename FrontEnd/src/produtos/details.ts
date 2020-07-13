import { autoinject } from 'aurelia-framework';
import { Produto } from "models/produto";
import { ProdutoService } from 'services/ProdutoService';

@autoinject
export class Details {
    produto: Produto;
    produtoService: ProdutoService;

    constructor(produtoService: ProdutoService) {
        this.produtoService = produtoService;
    }

    activate(params, routeConfig) {
        this.produtoService.get(params.id).then((response) => {
            if (!response.error)
                this.produto = response.response;
        });


    }
}
