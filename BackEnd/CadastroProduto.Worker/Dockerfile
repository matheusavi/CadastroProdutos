#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CadastroProduto.Worker/CadastroProduto.Worker.csproj", "CadastroProduto.Worker/"]
COPY ["CadastroProduto.CQS/CadastroProduto.CQS.csproj", "CadastroProduto.CQS/"]
RUN dotnet restore "CadastroProduto.Worker/CadastroProduto.Worker.csproj"
COPY . .
WORKDIR "/src/CadastroProduto.Worker"
RUN dotnet build "CadastroProduto.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CadastroProduto.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadastroProduto.Worker.dll"]
