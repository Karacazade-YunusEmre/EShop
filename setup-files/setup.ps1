# EShop Clean Architecture - Setup Script (PowerShell)

dotnet new sln --name EShop --output EShop
Set-Location EShop

dotnet new classlib --framework net10.0 --name EShop.Domain         --output src/EShop.Domain
dotnet new classlib --framework net10.0 --name EShop.Application    --output src/EShop.Application
dotnet new classlib --framework net10.0 --name EShop.Infrastructure --output src/EShop.Infrastructure
dotnet new webapi   --framework net10.0 --name EShop.Api            --output src/EShop.Api

dotnet new xunit --framework net10.0 --name EShop.Domain.UnitTests         --output tests/EShop.Domain.UnitTests
dotnet new xunit --framework net10.0 --name EShop.Application.UnitTests    --output tests/EShop.Application.UnitTests
dotnet new xunit --framework net10.0 --name EShop.Api.IntegrationTests     --output tests/EShop.Api.IntegrationTests

dotnet sln add src/EShop.Domain/EShop.Domain.csproj
dotnet sln add src/EShop.Application/EShop.Application.csproj
dotnet sln add src/EShop.Infrastructure/EShop.Infrastructure.csproj
dotnet sln add src/EShop.Api/EShop.Api.csproj
dotnet sln add tests/EShop.Domain.UnitTests/EShop.Domain.UnitTests.csproj
dotnet sln add tests/EShop.Application.UnitTests/EShop.Application.UnitTests.csproj
dotnet sln add tests/EShop.Api.IntegrationTests/EShop.Api.IntegrationTests.csproj

dotnet add src/EShop.Application/EShop.Application.csproj reference src/EShop.Domain/EShop.Domain.csproj
dotnet add src/EShop.Infrastructure/EShop.Infrastructure.csproj reference src/EShop.Application/EShop.Application.csproj
dotnet add src/EShop.Api/EShop.Api.csproj reference src/EShop.Application/EShop.Application.csproj
dotnet add src/EShop.Api/EShop.Api.csproj reference src/EShop.Infrastructure/EShop.Infrastructure.csproj
dotnet add tests/EShop.Domain.UnitTests/EShop.Domain.UnitTests.csproj           reference src/EShop.Domain/EShop.Domain.csproj
dotnet add tests/EShop.Application.UnitTests/EShop.Application.UnitTests.csproj reference src/EShop.Application/EShop.Application.csproj
dotnet add tests/EShop.Api.IntegrationTests/EShop.Api.IntegrationTests.csproj   reference src/EShop.Api/EShop.Api.csproj
