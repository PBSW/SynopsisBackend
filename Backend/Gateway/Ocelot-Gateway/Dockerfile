﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["/Backend/Gateway/Ocelot-Gateway/Ocelot-Gateway.csproj", "Ocelot-Gateway/"]
RUN dotnet restore "Ocelot-Gateway/Ocelot-Gateway.csproj"

COPY . .
WORKDIR "/src/Backend/Gateway/Ocelot-Gateway"

RUN dotnet build "Ocelot-Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ocelot-Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelot-Gateway.dll"]
