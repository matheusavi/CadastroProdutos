A simple product registration application with data synchronization using a message-broker.

The application is divided into 4 projects, namely:
  
1 - CadastroProduto.Api.Principal  
Product registration API in [ASP.NET Core](https://github.com/dotnet/aspnetcore).  
Data persists in [PostgreSQL](https://www.postgresql.org/).  
This API is divided into layers.
It emits domain events when the product is modified and sends this to [RabbitMQ](https://www.rabbitmq.com/) using the [MassTransit](https://masstransit-project.com/) library.  

2 - CadastroProduto.Worker  
Simple worker, listens for events from some RabbitMQ queues using MassTransit and sends this data to a second API.

3 - CadastroProdutos.Api  
Simple api that saves received data in MongoDB.

4 - FrontEnd  
Made in [aurelia](https://aurelia.io/), another SPA framework.
Connects with the backend API to create, list and edit the products.

To run the application, just have docker installed on your machine, enter the backend folder and run the command `docker-compose up`.
**This is going to take quite a while**. All necessary applications will be started (including message brokers and database).
It is also possible to open the solution located in Backend in visual studio and run it from docker compose.
After that, just access the address localhost:8080 in your browser and you will have access to the frontend application.

⚠️ On the first run it is possible that the API container does not go up due to the delay in downloading the entire environment, just run it again and the process will be fast.
