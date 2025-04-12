# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Build stage 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /app
COPY ["./Socialix/Socialix.csproj", "./Socialix/"]
RUN --mount=type=cache,target=/root/.nuget/packages \
    dotnet restore "./Socialix/Socialix.csproj"
COPY ./Socialix ./Socialix
WORKDIR "/app/Socialix"
RUN dotnet build "./Socialix.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish app stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Socialix.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_URLS="http://+:8080;http://+:8081"
ENV ASPNETCORE_ENVIRONMENT="Development"

ENTRYPOINT ["dotnet", "Socialix.dll"]