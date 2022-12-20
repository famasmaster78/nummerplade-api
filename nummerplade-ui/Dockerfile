#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nummerplade-ui/nummerplade-ui.csproj", "nummerplade-ui/"]
COPY ["nummerplade-api/nummerplade-api.csproj", "nummerplade-api/"]
RUN dotnet restore "nummerplade-ui/nummerplade-ui.csproj"
COPY . .
WORKDIR "/src/nummerplade-ui"
RUN dotnet build "nummerplade-ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "nummerplade-ui.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "nummerplade-ui.dll"]
