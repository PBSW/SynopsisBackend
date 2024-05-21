FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["/Backend/Gateway/YARP/YARP.csproj", "YARP/"]

COPY . .
WORKDIR "/src/Backend/Gateway/YARP"

RUN dotnet restore "YARP.csproj"

RUN dotnet build "/src/Backend/Gateway/YARP/YARP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/Backend/Gateway/YARP/YARP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YARP.dll"]
