FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

USER $APP_UID
WORKDIR /app
EXPOSE 23456
EXPOSE 23457

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PrintersManager/PrintersManager.csproj", "PrintersManager/"]
RUN dotnet restore "PrintersManager/PrintersManager.csproj"
COPY . .
WORKDIR "/src/PrintersManager"
RUN dotnet build "PrintersManager.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "PrintersManager.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PrintersManager.dll"]