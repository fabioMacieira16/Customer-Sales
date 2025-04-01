FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiDDD.Api/SalesAPI.Api.csproj", "ApiDDD.Api/"]
COPY ["ApiDDD.Application/SalesAPI.Application.csproj", "ApiDDD.Application/"]
COPY ["ApiDDD.Data/SalesAPI.Infrastructure.csproj", "ApiDDD.Data/"]
COPY ["ApiDDD.Domain/SalesAPI.Domain.csproj", "ApiDDD.Domain/"]
RUN dotnet restore "ApiDDD.Api/SalesAPI.Api.csproj"
COPY . .
WORKDIR "/src/ApiDDD.Api"
RUN dotnet build "SalesAPI.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesAPI.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesAPI.Api.dll"] 