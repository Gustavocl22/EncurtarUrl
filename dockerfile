FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["src/UrlShortenerApi/UrlShortenerApi.csproj", "UrlShortenerApi/"]
RUN dotnet restore "./UrlShortenerApi/UrlShortenerApi.csproj"

COPY . .
WORKDIR "/src/UrlShortenerApi"
RUN dotnet build "UrlShortenerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UrlShortenerApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "UrlShortenerApi.dll"]
