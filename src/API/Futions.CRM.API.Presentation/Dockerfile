# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/API/Futions.CRM.API.Presentation/Futions.CRM.API.Presentation.csproj", "src/API/Futions.CRM.API.Presentation/"]
COPY ["src/Common/Futions.CRM.Common.Presentation/Futions.CRM.Common.Presentation.csproj", "src/Common/Futions.CRM.Common.Presentation/"]
COPY ["src/Common/Futions.CRM.Common.Domain/Futions.CRM.Common.Domain.csproj", "src/Common/Futions.CRM.Common.Domain/"]
COPY ["src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Infrastructure/Futions.CRM.Modules.Catalogue.Infrastructure.csproj", "src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Infrastructure/"]
COPY ["src/Common/Futions.CRM.Common.Infrastructure/Futions.CRM.Common.Infrastructure.csproj", "src/Common/Futions.CRM.Common.Infrastructure/"]
COPY ["src/Common/Futions.CRM.Common.Application/Futions.CRM.Common.Application.csproj", "src/Common/Futions.CRM.Common.Application/"]
COPY ["src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Application/Futions.CRM.Modules.Catalogue.Application.csproj", "src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Application/"]
COPY ["src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Domain/Futions.CRM.Modules.Catalogue.Domain.csproj", "src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Domain/"]
COPY ["src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Presentation/Futions.CRM.Modules.Catalogue.Presentation.csproj", "src/Modules/ProductBook/Futions.CRM.Modules.ProductBook.Presentation/"]
RUN dotnet restore "./src/API/Futions.CRM.API.Presentation/Futions.CRM.API.Presentation.csproj"
COPY . .
WORKDIR "/src/src/API/Futions.CRM.API.Presentation"
RUN dotnet build "./Futions.CRM.API.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Futions.CRM.API.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Futions.CRM.API.Presentation.dll"]