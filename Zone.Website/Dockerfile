#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Zone.Website/Zone.Website.csproj", "Zone.Website/"]
COPY ["Zone.Core/Zone.Core.csproj", "Zone.Core/"]
COPY ["Spotify.OAuth/Spotify.OAuth.csproj", "Spotify.OAuth/"]
RUN dotnet restore "Zone.Website/Zone.Website.csproj"
COPY . .
WORKDIR "/src/Zone.Website"
RUN dotnet build "Zone.Website.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zone.Website.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zone.Website.dll"]