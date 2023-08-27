# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8000

# Use the official .NET Core SDK as the build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
#COPY ReadingIsGood.Api/*.csproj ./src/ReadingIsGood.Api/
#COPY ReadingIsGood.Application/*.csproj ./src/ReadingIsGood.Application/
#COPY ReadingIsGood.Domain/*.csproj ./src/ReadingIsGood.Domain/
#COPY ReadingIsGood.Infrastructure/*.csproj ./src/ReadingIsGood.Infrastructure/
#COPY tests/ApiTests/*.csproj ./tests/ApiTests/
#COPY tests/ApplicationTests/*.csproj ./tests/ApplicationTests/
#COPY tests/DomainTests/*.csproj ./tests/DomainTests/
#COPY tests/InfrastructureTests/*.csproj ./tests/InfrastructureTests/

# Copy the entire solution and build
COPY . ./
RUN dotnet publish ReadingIsGood.Api/ReadingIsGood.Api.csproj -c Release -o /app/out

# Use the base image for the final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "ReadingIsGood.Api.dll"]
