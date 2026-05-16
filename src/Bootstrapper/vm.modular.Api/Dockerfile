# syntax=docker/dockerfile:1

# ============================================================
# Base runtime
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl \
    && rm -rf /var/lib/apt/lists/*

RUN useradd -m appuser
USER appuser

# ============================================================
# Build stage
# ============================================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Build argument for GitHub Packages authentication
ARG GITHUB_TOKEN

# Copy Bootstrapper (main API)
COPY ["src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj", "src/Bootstrapper/vm.modular.Api/"]

# Copy Basket module projects
COPY ["src/Modules/Basket/Basket.Application/Basket.Application.csproj", "src/Modules/Basket/Basket.Application/"]
COPY ["src/Modules/Basket/Basket.Domain/Basket.Domain.csproj", "src/Modules/Basket/Basket.Domain/"]
COPY ["src/Modules/Basket/Basket.Infrastructure/Basket.Infrastructure.csproj", "src/Modules/Basket/Basket.Infrastructure/"]
COPY ["src/Modules/Basket/Basket.Module/Basket.Module.csproj", "src/Modules/Basket/Basket.Module/"]

# Copy Catalog module projects
COPY ["src/Modules/Catalog/Catalog.Application/Catalog.Application.csproj", "src/Modules/Catalog/Catalog.Application/"]
COPY ["src/Modules/Catalog/Catalog.Domain/Catalog.Domain.csproj", "src/Modules/Catalog/Catalog.Domain/"]
COPY ["src/Modules/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj", "src/Modules/Catalog/Catalog.Infrastructure/"]
COPY ["src/Modules/Catalog/Catalog.Module/Catalog.Module.csproj", "src/Modules/Catalog/Catalog.Module/"]

# Copy Ordering module projects
COPY ["src/Modules/Ordering/Ordering.Application/Ordering.Application.csproj", "src/Modules/Ordering/Ordering.Application/"]
COPY ["src/Modules/Ordering/Ordering.Domain/Ordering.Domain.csproj", "src/Modules/Ordering/Ordering.Domain/"]
COPY ["src/Modules/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "src/Modules/Ordering/Ordering.Infrastructure/"]
COPY ["src/Modules/Ordering/Ordering.Module/Ordering.Module.csproj", "src/Modules/Ordering/Ordering.Module/"]
COPY src/NuGet.config ./




# ============================================================
# Inject GitHub token into NuGet.config
# Replace %GITHUB_TOKEN% placeholder in NuGet.config
# ============================================================
RUN sed -i "s|%GITHUB_TOKEN%|${GITHUB_TOKEN}|g" NuGet.config

# ============================================================
# Restore dependencies
# ============================================================
RUN dotnet restore "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" \
    --configfile NuGet.config

# ============================================================
# Copy full source
# ============================================================
COPY . .

# ============================================================
# Build
# ============================================================
RUN dotnet build "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" \
    -c Release \
    -o /app/build \
    --no-restore

# ============================================================
# Publish
# ============================================================
FROM build AS publish

RUN dotnet publish "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" \
    -c Release \
    -o /app/publish \
     /p:UseAppHost=false

# ============================================================
# Final runtime
# ============================================================
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

HEALTHCHECK CMD curl --fail http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "vm.modular.Api.dll"]