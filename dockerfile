# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o projeto e restaura dependências
COPY ["UrlShortenerApi.csproj", "./"]
RUN dotnet restore

# Copia todo o código
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Define que o ASP.NET vai rodar na porta padrão do Render automaticamente
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
