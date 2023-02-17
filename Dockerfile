FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /App

# Copy everything
COPY MediaRenamer ./MediaRenamer
COPY MediaRenamer.Common ./MediaRenamer.Common
COPY MediaRenamer.Media ./MediaRenamer.Media
COPY MediaRenamer.TMDb ./MediaRenamer.TMDb
COPY MediaRenamer.TvMaze ./MediaRenamer.TvMaze

# Build
WORKDIR /App/MediaRenamer
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App
COPY --from=build-env /App/MediaRenamer/out .
ENTRYPOINT ["dotnet", "MediaRenamer.dll"]
