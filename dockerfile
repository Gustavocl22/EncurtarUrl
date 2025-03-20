# Usar a imagem base do .NET SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Definir o diretório de trabalho no contêiner
WORKDIR /app

# Copiar o diretório inteiro de src/UrlShortenerApi para o contêiner
COPY ./UrlShortenerApi /app/UrlShortenerApi

# Restaurar dependências
RUN dotnet restore /app/UrlShortenerApi/UrlShortenerApi.csproj

# Compilar o projeto
RUN dotnet publish /app/UrlShortenerApi/UrlShortenerApi.csproj -c Release -o /app/publish

# Usar a imagem base do .NET Runtime para produção
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Definir o diretório de trabalho no contêiner
WORKDIR /app

# Copiar os arquivos publicados para a imagem final
COPY --from=build /app/publish .

# Expor a porta 80
EXPOSE 80

# Comando para rodar a API
ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
