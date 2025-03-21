# Etapa de base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY UrlShortenerApi.csproj ./
RUN dotnet restore "UrlShortenerApi.csproj"

COPY . . 
RUN dotnet build "UrlShortenerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortenerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
