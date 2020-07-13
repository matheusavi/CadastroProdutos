export class Produto {
    public nome: string;
    public preco: number;
    public estoque: number;

    constructor(nome: string,
        preco: number,
        estoque: number) {
        this.nome = nome;
        this.preco = preco;
        this.estoque = estoque;
    }
}
