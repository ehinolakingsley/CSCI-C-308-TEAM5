# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 2024


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CSCI-C-308-PROJECT/CSCI-C-308-TEAM5.API.csproj", "CSCI-C-308-PROJECT/"]
RUN dotnet restore "./CSCI-C-308-PROJECT/CSCI-C-308-TEAM5.API.csproj"
COPY . .
WORKDIR "/src/CSCI-C-308-PROJECT"
RUN dotnet build "./CSCI-C-308-TEAM5.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CSCI-C-308-TEAM5.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CSCI-C-308-TEAM5.API.dll"]