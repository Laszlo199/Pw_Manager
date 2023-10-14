FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Pw_WebApi/Pw_WebApi.csproj", "Pw_WebApi/"]
COPY ["Pw_Security/Pw_Security.csproj", "Pw_Security/"]
COPY ["DataAcces/DataAcces.csproj", "DataAcces/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "Pw_WebApi/Pw_WebApi.csproj"
COPY . .
WORKDIR "/src/Pw_WebApi"
RUN dotnet build "Pw_WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pw_WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pw_WebApi.dll"]
