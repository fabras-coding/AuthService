# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files for better layer caching
COPY AuthService.sln ./
COPY AuthService.Domain/AuthService.Domain.csproj ./AuthService.Domain/
COPY AuthService.Application/AuthService.Application.csproj ./AuthService.Application/
COPY AuthService.Infra.Data/AuthService.Infra.Data.csproj ./AuthService.Infra.Data/
COPY AuthService.Infra.IoC/AuthService.Infra.IoC.csproj ./AuthService.Infra.IoC/
COPY AuthService.WebAPI/AuthService.WebAPI.csproj ./AuthService.WebAPI/
COPY AuthService.Tests/AuthService.Tests.csproj ./AuthService.Tests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the WebAPI project
RUN dotnet publish AuthService.WebAPI/AuthService.WebAPI.csproj -c Release -o /app --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

# Expose port
EXPOSE 80

# Set the correct entry point
ENTRYPOINT ["dotnet", "AuthService.WebAPI.dll"]
