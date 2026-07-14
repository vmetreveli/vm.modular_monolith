# syntax=docker/dockerfile:1

# ============================================================
# Base runtime
# ============================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

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

ARG GITHUB_TOKEN

# Copy project files (for caching restore layer)
COPY src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj src/Bootstrapper/vm.modular.Api/

COPY src/Modules/Basket/Basket.Application/*.csproj src/Modules/Basket/Basket.Application/
COPY src/Modules/Basket/Basket.Domain/*.csproj src/Modules/Basket/Basket.Domain/
COPY src/Modules/Basket/Basket.Infrastructure/*.csproj src/Modules/Basket/Basket.Infrastructure/
COPY src/Modules/Basket/Basket.Module/*.csproj src/Modules/Basket/Basket.Module/

COPY src/Modules/Catalog/Catalog.Application/*.csproj src/Modules/Catalog/Catalog.Application/
COPY src/Modules/Catalog/Catalog.Domain/*.csproj src/Modules/Catalog/Catalog.Domain/
COPY src/Modules/Catalog/Catalog.Infrastructure/*.csproj src/Modules/Catalog/Catalog.Infrastructure/
COPY src/Modules/Catalog/Catalog.Module/*.csproj src/Modules/Catalog/Catalog.Module/

COPY src/Modules/Ordering/Ordering.Application/*.csproj src/Modules/Ordering/Ordering.Application/
COPY src/Modules/Ordering/Ordering.Domain/*.csproj src/Modules/Ordering/Ordering.Domain/
COPY src/Modules/Ordering/Ordering.Infrastructure/*.csproj src/Modules/Ordering/Ordering.Infrastructure/
COPY src/Modules/Ordering/Ordering.Module/*.csproj src/Modules/Ordering/Ordering.Module/

# Copy NuGet config
COPY NuGet.config .

# Safely inject token only if provided
RUN if [ -n "$GITHUB_TOKEN" ]; then \
        sed -i "s|%GITHUB_TOKEN%|$GITHUB_TOKEN|g" NuGet.config ; \
    fi

# Restore
RUN dotnet restore "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" --configfile "NuGet.config"

# Copy full source
COPY . .

# Build
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

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s \
  CMD curl -f http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "vm.modular.Api.dll"]