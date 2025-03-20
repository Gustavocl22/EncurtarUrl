# Usar a imagem base do .NET SDK para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Definir o diretório de trabalho dentro do contêiner
WORKDIR /src

# Copiar o arquivo .csproj para dentro do contêiner
COPY ./UrlShortenerApi/UrlShortenerApi.csproj ./UrlShortenerApi/

# Restaurar as dependências
RUN dotnet restore ./UrlShortenerApi/UrlShortenerApi.csproj

# Copiar o restante do código-fonte
COPY ./UrlShortenerApi/. ./UrlShortenerApi/

# Construir o projeto
RUN dotnet publish ./UrlShortenerApi/UrlShortenerApi.csproj -c Release -o /app/publish

# Usar a imagem base do .NET Runtime para produção
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

# Definir o diretório de trabalho no contêiner
WORKDIR /app

# Copiar os arquivos compilados da imagem de build
COPY --from=build /app/publish .


# Expor a porta 80
EXPOSE 80

# Comando para rodar o aplicativo
ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
