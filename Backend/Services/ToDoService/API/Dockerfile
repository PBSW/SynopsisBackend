FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["/Backend/Services/ToDoService/API/API.csproj", "API/"]
COPY ["/Backend/Services/ToDoService/Application/Application.csproj", "Application/"]
COPY ["/Backend/Services/Shared/Shared.csproj", "Shared/"]
COPY ["/Backend/Services/ToDoService/Infrastructure/Infrastructure.csproj", "Infrastructure/"]

COPY . .
WORKDIR "/src/Backend/Services/ToDoService/API"

RUN dotnet restore "API.csproj"

RUN dotnet build "/src/Backend/Services/ToDoService/API/API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/src/Backend/Services/ToDoService/API/API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]
