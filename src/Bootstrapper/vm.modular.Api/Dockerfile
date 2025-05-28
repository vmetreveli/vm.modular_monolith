# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Disable unwanted logging and re-use behavior
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1 \
    MSBUILDDISABLENODEREUSE=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=true

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj", "src/Bootstrapper/vm.modular.Api/"]
COPY ["src/Framework/Framework.Infrastructure/Framework.Infrastructure.csproj", "src/Framework/Framework.Infrastructure/"]
COPY ["src/Framework/Framework.Abstractions/Framework.Abstractions.csproj", "src/Framework/Framework.Abstractions/"]
COPY ["src/Modules/Catalog/Catalog.Module/Catalog.Module.csproj", "src/Modules/Catalog/Catalog.Module/"]
COPY ["src/Modules/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Modules/Catalog/Catalog.Application/"]
COPY ["src/Modules/Catalog/Catalog.Domain/Catalog.Domain.csproj", "src/Modules/Catalog/Catalog.Domain/"]
COPY ["src/Integration/AsynchronousAdapter/AsynchronousAdapter.csproj", "src/Integration/AsynchronousAdapter/"]
COPY ["src/Modules/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/Modules/Catalog/Catalog.Infrastructure/"]

# Copy everything
COPY . .

# Restore & Build
RUN dotnet restore "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj"
RUN dotnet build "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vm.modular.Api.dll"]
