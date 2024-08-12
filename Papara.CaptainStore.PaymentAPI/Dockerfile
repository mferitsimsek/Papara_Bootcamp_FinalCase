FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Papara.CaptainStore.PaymentAPI/Papara.CaptainStore.PaymentAPI.csproj", "Papara.CaptainStore.PaymentAPI/"]
COPY ["Core/Papara.CaptainStore.Application/Papara.CaptainStore.Application.csproj", "Core/Papara.CaptainStore.Application/"]
COPY ["Core/Papara.CaptainStore.Domain/Papara.CaptainStore.Domain.csproj", "Core/Papara.CaptainStore.Domain/"]
COPY ["Core/Papara.CaptainStore.ElasticSearch/Papara.CaptainStore.ElasticSearch.csproj", "Core/Papara.CaptainStore.ElasticSearch/"]
COPY ["Infrastructure/Papara.CaptainStore.Persistence/Papara.CaptainStore.Persistence.csproj", "Infrastructure/Papara.CaptainStore.Persistence/"]
RUN dotnet restore "./Papara.CaptainStore.PaymentAPI/Papara.CaptainStore.PaymentAPI.csproj"
COPY . .
WORKDIR "/src/Papara.CaptainStore.PaymentAPI"
RUN dotnet build "./Papara.CaptainStore.PaymentAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Papara.CaptainStore.PaymentAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Papara.CaptainStore.PaymentAPI.dll"]