# Imagem base do .NET SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar tudo
COPY . .

# Restaurar dependências e publicar
RUN dotnet restore UrlShortenerApi.csproj
RUN dotnet publish UrlShortenerApi.csproj -c Release -o out

# Imagem runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Comando para iniciar
ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
