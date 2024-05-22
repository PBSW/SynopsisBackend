FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["/Backend/Gateway/YARP-Gateway/YARP-Gateway.csproj", "YARP-Gateway/"]
RUN dotnet restore "YARP-Gateway/YARP-Gateway.csproj"

COPY . .
WORKDIR "/src/Backend/Gateway/YARP-Gateway"

RUN dotnet build "YARP-Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YARP-Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YARP-Gateway.dll"]
