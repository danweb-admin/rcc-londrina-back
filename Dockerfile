FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "RccManager.sln" "RccManager.sln"

COPY "RccManager.API/RccManager.API.csproj" "RccManager.API/RccManager.API.csproj"
COPY "RccManager.Application/RccManager.Application.csproj" "RccManager.Application/RccManager.Application.csproj"
COPY "RccManager.Domain/RccManager.Domain.csproj" "RccManager.Domain/RccManager.Domain.csproj"
COPY "RccManager.Infra/RccManager.Infra.csproj" "RccManager.Infra/RccManager.Infra.csproj"
COPY "RccManager.Service/RccManager.Service.csproj" "RccManager.Service/RccManager.Service.csproj"
COPY "RccManager.Test/RccManager.Test.csproj" "RccManager.Test/RccManager.Test.csproj"
COPY "RccManager.API/Templates" "./Templates"


RUN dotnet restore "RccManager.sln"

COPY . .

WORKDIR "/src/RccManager.API"
RUN dotnet build "RccManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RccManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RccManager.API.dll"]