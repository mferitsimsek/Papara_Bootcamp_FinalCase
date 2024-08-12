#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/Papara.CaptainStore.API/Papara.CaptainStore.API.csproj", "Presentation/Papara.CaptainStore.API/"]
COPY ["Infrastructure/Papara.CaptainStore.Persistence/Papara.CaptainStore.Persistence.csproj", "Infrastructure/Papara.CaptainStore.Persistence/"]
COPY ["Core/Papara.CaptainStore.Application/Papara.CaptainStore.Application.csproj", "Core/Papara.CaptainStore.Application/"]
COPY ["Core/Papara.CaptainStore.Domain/Papara.CaptainStore.Domain.csproj", "Core/Papara.CaptainStore.Domain/"]
COPY ["Core/Papara.CaptainStore.ElasticSearch/Papara.CaptainStore.ElasticSearch.csproj", "Core/Papara.CaptainStore.ElasticSearch/"]
RUN dotnet restore "./Presentation/Papara.CaptainStore.API/Papara.CaptainStore.API.csproj"
COPY . .
WORKDIR "/src/Presentation/Papara.CaptainStore.API"
RUN dotnet build "./Papara.CaptainStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Papara.CaptainStore.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Papara.CaptainStore.API.dll"]