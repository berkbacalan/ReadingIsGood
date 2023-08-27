FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ReadingIsGood.Api/ReadingIsGood.Api.csproj", "ReadingIsGood.Api/"]
RUN dotnet restore "ReadingIsGood.Api/ReadingIsGood.Api.csproj"
COPY . .
WORKDIR "/src/ReadingIsGood.Api"
RUN dotnet build "ReadingIsGood.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReadingIsGood.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadingIsGood.Api.dll"]
