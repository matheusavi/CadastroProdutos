import { Produto } from '../models/produto';
import { Router } from 'aurelia-router';
import { autoinject } from 'aurelia-dependency-injection';
import { ValidationControllerFactory, ValidationController, ValidationRules } from 'aurelia-validation';
import { BootstrapFormRenderer } from "../utilities/bootstrap-form-renderer";
import { ProdutoService } from 'services/ProdutoService';
import { DialogService } from 'aurelia-dialog';
import { Dialog } from 'dialog';
import { NumericDictionary } from 'lodash';

@autoinject
export class Cadastrar {
    controller: ValidationController;
    produtoService: ProdutoService;
    dialogService: DialogService;
    valid: boolean;
    router: Router;

    id: number;
    nome: string;
    preco: number;
    estoque: number;

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
                if (this.id > 0) {
                    let produto = new Produto(this.nome, this.preco, this.estoque);
                    produto.id = this.id;
                    let response = await this.produtoService.update(produto);
                    if (!response.error) {
                        this.router.navigateToRoute("details", { id: this.id });
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
                } else {
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
            }
        });
    }

    activate(params, routeConfig) {
        if (params.id > 0) {
            this.produtoService.get(params.id).then((response) => {
                if (!response.error) {
                    this.id = params.id;
                    this.nome = response.response.nome;
                    this.preco = response.response.preco;
                    this.estoque = response.response.estoque;
                } else {
                    alert('Ocorreu um erro ao editar o produto');
                }
            });
        }
    }

    toCamelCase(value: String) {
        return value
            .replace(/\s(.)/g, function ($1) { return $1.toUpperCase(); })
            .replace(/\s/g, '')
            .replace(/^(.)/, function ($1) { return $1.toLowerCase(); });
    }

}
