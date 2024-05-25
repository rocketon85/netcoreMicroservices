#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["netcoremicroservices.auth/NetCoreMicroservices.Auth.csproj", "netcoremicroservices.auth/"]
COPY ["netcoremicroservices.commons/NetCoreMicroservices.Commons.csproj", "netcoremicroservices.commons/"]
RUN dotnet restore "./netcoremicroservices.auth/NetCoreMicroservices.Auth.csproj"
RUN dotnet ef
COPY . .
WORKDIR "/src/netcoremicroservices.auth"
RUN dotnet build "./NetCoreMicroservices.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NetCoreMicroservices.Auth.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#CMD ["dotnet", "ef", "--project", "../netcoremicroservices.auth", "database", "update"]

FROM base as migrations
RUN dotnet tool install --version 6.0.9 --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT dotnet-ef database update --project netcoremicroservices.auth/ --startup-project src/netcoremicroservices.auth/

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreMicroservices.Auth.dll"]
