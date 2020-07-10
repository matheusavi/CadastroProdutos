#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CadastroProduto.Api.Principal/CadastroProduto.Api.Principal.csproj", "CadastroProduto.Api.Principal/"]
RUN dotnet restore "CadastroProduto.Api.Principal/CadastroProduto.Api.Principal.csproj"
COPY . .
WORKDIR "/src/CadastroProduto.Api.Principal"
RUN dotnet build "CadastroProduto.Api.Principal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CadastroProduto.Api.Principal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CadastroProduto.Api.Principal.dll"]