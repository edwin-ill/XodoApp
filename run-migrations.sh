set -e

dotnet ef database update --project XodoApp.Infrastructure.Persistence/XodoApp.Infrastructure.Persistence.csproj --startup-project XodoApp.WebApi/XodoApp.WebApi.csproj

dotnet ef database update --project XodoApp.Infrastructure.Identity/XodoApp.Infrastructure.Identity.csproj --startup-project XodoApp.WebApi/XodoApp.WebApi.csproj