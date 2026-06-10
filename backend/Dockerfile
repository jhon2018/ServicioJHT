# 1. Etapa de construcción (Build)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de solución y proyectos
COPY ["JHT.Logistics.sln", "./"]
COPY ["JHT.Logistics.API/JHT.Logistics.API.csproj", "JHT.Logistics.API/"]
COPY ["JHT.Logistics.Application/JHT.Logistics.Application.csproj", "JHT.Logistics.Application/"]
COPY ["JHT.Logistics.Domain/JHT.Logistics.Domain.csproj", "JHT.Logistics.Domain/"]
COPY ["JHT.Logistics.Infrastructure/JHT.Logistics.Infrastructure.csproj", "JHT.Logistics.Infrastructure/"]

# Restaurar dependencias
RUN dotnet restore "JHT.Logistics.API/JHT.Logistics.API.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/JHT.Logistics.API"
RUN dotnet build "JHT.Logistics.API.csproj" -c Release -o /app/build

# 2. Etapa de publicación
FROM build AS publish
RUN dotnet publish "JHT.Logistics.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. Etapa de ejecución (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080

# Variables de entorno por defecto para Render
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JHT.Logistics.API.dll"]
