Uma aplicação de cadastro de produtos simples com sincronização.  
A aplicação está dividida em 4 projetos, sendo eles:
  
1 - CadastroProduto.Api.Principal  
API de cadastro de produtos em [ASP.NET Core](https://github.com/dotnet/aspnetcore).  
Persiste os dados em [PostgreSQL](https://www.postgresql.org/).  
Esta API está dividida em camadas.  
Ela emite eventos de domínio quando o produto é modificado e envia isso para o [RabbitMQ](https://www.rabbitmq.com/) utilizando a biblioteca [MassTransit](https://masstransit-project.com/).  

2 - CadastroProduto.Worker  
Worker simples, escuta os eventos de algumas filas do RabbitMQ utilizando também o MassTransit e envia estes dados para uma segunda API.

3 - CadastroProdutos.Api  
Api simples que salva os dados recebidos em MongoDB.

4 - FrontEnd  
Feito em [aurelia](https://aurelia.io/), mais um framework SPA.  
Conecta-se com a API do backend para criar, listar e editar os produtos.

Para executar a aplicação basta ter o docker instalado em sua máquina, entrar na pasta backend e executar o comando `docker-compose up`.   
**Isso vai levar um bom tempo**. Todas as aplicações necessárias serão iniciadas (incluindo message brokers, banco de dados e afins).  
Também é possível abrir a solução localizada em Backend no visual studio e executar ela a partir do docker compose.  
Após isso basta acessar o endereço localhost:8080 em seu navegador que você terá acesso a aplicação frontend.
  

⚠️ Na primeira execução é possível que o container da API não suba devido a demora para baixar todo o ambiente, basta executar novamente que o processo será rápido.  
