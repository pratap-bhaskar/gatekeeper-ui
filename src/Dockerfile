FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["GatekeeperView/GatekeeperView.csproj", "GatekeeperView/"]
RUN dotnet restore "GatekeeperView/GatekeeperView.csproj"
COPY . .
WORKDIR "/src/GatekeeperView"
RUN dotnet build "GatekeeperView.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GatekeeperView.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatekeeperView.dll"]