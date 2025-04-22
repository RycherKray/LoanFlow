FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Use SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy everything including subfolders (src/, portal/, etc.)
COPY . .

# Restore dependencies using the solution
RUN dotnet restore LoanFlow.sln

# Publish only the API project
RUN dotnet publish ./src/LoanFlow.Api/LoanFlow.Api.csproj -c Release -o /app/publish

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LoanFlow.Api.dll"]