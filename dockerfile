# Usando a imagem oficial do .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Copiando e publicando a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["UrlShortenerApi/UrlShortenerApi.csproj", "UrlShortenerApi/"]
RUN dotnet restore "UrlShortenerApi/UrlShortenerApi.csproj"
COPY . .
WORKDIR "/src/UrlShortenerApi"
RUN dotnet publish -c Release -o /app/publish

# Construindo a imagem final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
