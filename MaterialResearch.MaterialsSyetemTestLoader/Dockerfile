FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MaterialResearch.MaterialsSyetemTestLoader/MaterialResearch.MaterialsSyetemTestLoader.csproj", "MaterialResearch.MaterialsSyetemTestLoader/"]
RUN dotnet restore "MaterialResearch.MaterialsSyetemTestLoader/MaterialResearch.MaterialsSyetemTestLoader.csproj"
COPY . .
WORKDIR "/src/MaterialResearch.MaterialsSyetemTestLoader"
RUN dotnet build "MaterialResearch.MaterialsSyetemTestLoader.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MaterialResearch.MaterialsSyetemTestLoader.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaterialResearch.MaterialsSyetemTestLoader.dll"]
