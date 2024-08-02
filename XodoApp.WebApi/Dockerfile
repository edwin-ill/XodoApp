FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["XodoApp.WebApi/XodoApp.WebApi.csproj", "XodoApp.WebApi/"]
COPY ["XodoApp.Core.Application/XodoApp.Core.Application.csproj", "XodoApp.Core.Application/"]
COPY ["XodoApp.Core.Domain/XodoApp.Core.Domain.csproj", "XodoApp.Core.Domain/"]
COPY ["XodoApp.Infrastructure.Identity/XodoApp.Infrastructure.Identity.csproj", "XodoApp.Infrastructure.Identity/"]
COPY ["XodoApp.Infrastructure.Shared/XodoApp.Infrastructure.Shared.csproj", "XodoApp.Infrastructure.Shared/"]
COPY ["XodoApp.Infrastructure.Persistence/XodoApp.Infrastructure.Persistence.csproj", "XodoApp.Infrastructure.Persistence/"]
RUN dotnet restore "./XodoApp.WebApi/XodoApp.WebApi.csproj"
COPY . .
WORKDIR "/src/XodoApp.WebApi"
RUN dotnet build "./XodoApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./XodoApp.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools
ENV PATH="$PATH:/root/.dotnet/tools"

# Set the entry point to run migrations and start the application
ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "../XodoApp.Infrastructure.Persistence/XodoApp.Infrastructure.Persistence.csproj", "--startup-project", "XodoApp.WebApi.dll", "&&", \
            "dotnet", "ef", "database", "update", "--project", "../XodoApp.Infrastructure.Identity/XodoApp.Infrastructure.Identity.csproj", "--startup-project", "XodoApp.WebApi.dll", "&&", \
            "dotnet", "XodoApp.WebApi.dll"]