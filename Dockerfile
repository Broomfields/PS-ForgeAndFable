# syntax=docker/dockerfile:1

# ---- Build stage: restore + publish the web project ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore in its own layer for build caching.
COPY PS-ForgeAndFable/PS-ForgeAndFable.csproj PS-ForgeAndFable/
RUN dotnet restore PS-ForgeAndFable/PS-ForgeAndFable.csproj

# Publish only the web project (not the solution or test project). This copies
# RCL assets (e.g. _content/MudBlazor) physically into publish/wwwroot/_content.
COPY PS-ForgeAndFable/ PS-ForgeAndFable/
RUN dotnet publish PS-ForgeAndFable/PS-ForgeAndFable.csproj -c Release -o /app/publish

# ---- Runtime stage: ASP.NET image (dotnet + runtime already on PATH) ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_ENVIRONMENT=Production
# Railway injects $PORT at runtime; bind Kestrel to it (fallback 8080 for local runs).
CMD ["sh", "-c", "ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} dotnet PS-ForgeAndFable.dll"]
