# Etapa de build
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./UrlShortenerApi.csproj", "EncurtarUrl/UrlShortenerApi/"]
RUN dotnet restore "EncurtarUrl/UrlShortenerApi/UrlShortenerApi.csproj"
COPY . .
WORKDIR "/src/EncurtarUrl/UrlShortenerApi"
RUN dotnet build "UrlShortenerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortenerApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]

