FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj", "src/Bootstrapper/vm.modular.Api/"]
COPY ["../Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "../Framework/Framework.Infrastructure/"]
COPY ["../Framework/Framework.Abstractions/Framework.Abstractions.csproj", "../Framework/Framework.Abstractions/"]
COPY ["src/Modules/Catalog/Catalog.Module/Catalog.Module.csproj", "src/Modules/Catalog/Catalog.Module/"]
COPY ["src/Modules/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Modules/Catalog/Catalog.Application/"]
COPY ["src/Modules/Catalog/Catalog.Domain/Catalog.Domain.csproj", "src/Modules/Catalog/Catalog.Domain/"]
COPY ["Integration/AsynchronousAdapter/AsynchronousAdapter.csproj", "Integration/AsynchronousAdapter/"]
COPY ["src/Modules/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/Modules/Catalog/Catalog.Infrastructure/"]
RUN dotnet restore "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj"
COPY . .
WORKDIR "/src/src/Bootstrapper/vm.modular.Api"
RUN dotnet build "./vm.modular.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./vm.modular.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vm.modular.Api.dll"]
