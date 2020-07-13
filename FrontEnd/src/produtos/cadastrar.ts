import { Produto } from '../models/produto';
import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-dependency-injection';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { BootstrapFormRenderer } from "../utilities/bootstrap-form-renderer";
import { ProdutoService } from 'services/ProdutoService';
import { DialogService } from 'aurelia-dialog';
import { Dialog } from 'dialog';

@autoinject
export class Cadastrar {
    controller: ValidationController;
    produtoService: ProdutoService;
    nome: string;
    preco: number;
    estoque: number;
    dialogService: DialogService;
    valid: boolean;
    router: Router;

    constructor(
        controllerFactory: ValidationControllerFactory,
        produtoService: ProdutoService,
        dialogService: DialogService,
        router: Router) {
        this.controller = controllerFactory.createForCurrentScope();
        this.produtoService = produtoService;
        this.dialogService = dialogService;
        this.router = router;

        this.controller.addRenderer(new BootstrapFormRenderer());
        this.configureValidationRules();
    }

    configureValidationRules() {
        ValidationRules
            .ensure('nome')
            .required()
            .withMessage('Nome é obrigatório')
            .ensure('preco')
            .min(0.1)
            .withMessage('Preço deve ser maior que zero')
            .required()
            .withMessage('Preço deve ser maior que zero')
            .ensure('estoque')
            .min(1)
            .withMessage('Estoque deve ser maior que zero')
            .required()
            .withMessage('Estoque deve ser maior que zero')
            .on(this);
    }

    criarProduto() {
        this.controller.validate().then(async result => {
            if (result.valid) {
                let produto = new Produto(this.nome, this.preco, this.estoque);
                let response = await this.produtoService.create(produto);
                if (!response.error) {
                    this.router.navigateToRoute("details", { id: response.response.id });
                } else if (response.status == 400) {
                    response.response.forEach(element => {
                        this.controller.addError(element.error, this, this.toCamelCase(element.property));
                    });
                } else {
                    this.dialogService.open({
                        viewModel: Dialog,
                        model: {
                            message: response.response,
                            okAction: () => { },
                            cancelAction: () => { }
                        }
                    });
                }
            }
        });
    }

    toCamelCase(value: String) {
        return value
            .replace(/\s(.)/g, function ($1) { return $1.toUpperCase(); })
            .replace(/\s/g, '')
            .replace(/^(.)/, function ($1) { return $1.toLowerCase(); });
    }

}
