FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["UrlShortenerApi/UrlShortenerApi.csproj", "UrlShortenerApi/"]
WORKDIR /UrlShortenerApi
RUN dotnet restore "UrlShortenerApi.csproj"


COPY . .
WORKDIR "/UrlShortenerApi"
RUN dotnet build "./UrlShortenerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortenerApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
