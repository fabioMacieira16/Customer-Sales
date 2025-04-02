FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["ApiDDD.Api/ApiDDD.Api.csproj", "ApiDDD.Api/"]
COPY ["ApiDDD.Domain/ApiDDD.Domain.csproj", "ApiDDD.Domain/"]
COPY ["ApiDDD.Data/ApiDDD.Data.csproj", "ApiDDD.Data/"]
RUN dotnet restore "ApiDDD.Api/ApiDDD.Api.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "ApiDDD.Api/ApiDDD.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "ApiDDD.Api/ApiDDD.Api.csproj" -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiDDD.Api.dll"] 