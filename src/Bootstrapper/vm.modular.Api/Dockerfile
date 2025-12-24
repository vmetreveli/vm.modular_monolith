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

# Install Azure Artifact Credential Provider
RUN wget -qO- https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash

ARG GITHUB_PAT
ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS="{\"endpointCredentials\": [{\"endpoint\":\"https://nuget.pkg.github.com/YOUR_GITHUB_USERNAME/index.json\", \"username\":\"docker\", \"password\":\"${GITHUB_PAT}\"}]}"

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy NuGet.config first
COPY ["NuGet.config", "./"]

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

# Restore packages (this will now work with GitHub Packages)
RUN dotnet restore "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj"

# Copy everything else
COPY . .

# Build
RUN dotnet build "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "src/Bootstrapper/vm.modular.Api/vm.modular.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vm.modular.Api.dll"]