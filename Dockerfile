# Stage 1: Restore and build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and src folder
COPY LoanFlow.sln ./
COPY ./src ./src

# Restore dependencies
RUN dotnet restore LoanFlow.sln

# Build
RUN dotnet publish LoanFlow.sln -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "LoanFlow.API.dll"]
