import { HttpClient, json } from 'aurelia-fetch-client';
import { autoinject } from 'aurelia-dependency-injection';
import { Produto } from 'models/produto';

@autoinject
export class ProdutoService {
    private http: HttpClient;

    constructor(http: HttpClient) {
        this.http = http;

        const baseUrl = 'https://localhost:5001/api/';

        http.configure(config => {
            config.withBaseUrl(baseUrl);
        })
    }


    public async create(produto: Produto) {
        try {
            let response = await this.http.fetch('produto', {
                method: 'post',
                body: json(produto)
            });
            let responseBody;

            try {
                responseBody = await response.json();
            }
            catch (error) {
                console.log('Error getting the response.' + error);
                responseBody = response.statusText;
            }

            if (responseBody == undefined || responseBody == null || responseBody == "")
                responseBody = response.status + ': ' + 'Ocorreu um erro ao criar o produto';

            return {
                response: responseBody,
                error: !response.ok,
                status: response.status
            };
        }
        catch (error) {
            console.log('Error adding produto.' + error);
        }
    }

    public async get(id: any) {
        let response = await this.http.fetch('produto/' + id, {
            method: 'get'
        });

        let responseBody;

        try {
            responseBody = await response.json();
        }
        catch (error) {
            console.log('Error getting the response.' + error);
            responseBody = response.statusText;
        }

        if (responseBody == undefined || responseBody == null || responseBody == "")
            responseBody = response.status + ': ' + 'Ocorreu um erro ao obter o produto';

        return {
            response: responseBody,
            error: !response.ok,
            status: response.status
        };
    }

    public async getAll() {
        let response = await this.http.fetch('produto/todos', {
            method: 'get'
        });

        let responseBody;

        try {
            responseBody = await response.json();
        }
        catch (error) {
            console.log('Error getting the response.' + error);
            responseBody = response.statusText;
        }

        if (responseBody == undefined || responseBody == null || responseBody == "")
            responseBody = response.status + ': ' + 'Ocorreu um erro ao obter os produtos';

        return {
            response: responseBody,
            error: !response.ok,
            status: response.status
        };
    }

    public async delete(id: any) {
        let response = await this.http.fetch('produto/' + id, {
            method: 'delete'
        });

        let responseBody;

        try {
            responseBody = await response.json();
        }
        catch (error) {
            console.log('Error getting the response.' + error);
            responseBody = response.statusText;
        }

        if (responseBody == undefined || responseBody == null || responseBody == "")
            responseBody = response.status + ': ' + 'Ocorreu um erro ao deletar o produto';

        return {
            response: responseBody,
            error: !response.ok,
            status: response.status
        };
    }
}
