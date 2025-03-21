# Use a imagem base do SDK do .NET para construir a aplicação
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copie os arquivos do projeto e restaure as dependências
COPY *.csproj .
RUN dotnet restore

# Copie todo o código e construa a aplicação
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Use a imagem base do runtime do .NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Defina a porta que a aplicação vai usar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "UrlshortenerApi.dll"]