#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 82
ENV ASPNETCORE_URLS=http://+:82

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Authorization_API/Authorization_API.csproj", "Authorization_API/"]
COPY ["Authorization_Services/Authorization_Services.csproj", "Authorization_Services/"]
COPY ["Authorization_Models/Authorization_Models.csproj", "Authorization_Models/"]
COPY ["Authorization_Data/Authorization_Data.csproj", "Authorization_Data/"]
RUN dotnet restore "Authorization_API/Authorization_API.csproj"
COPY . .
WORKDIR "/src/Authorization_API"
RUN dotnet build "Authorization_API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Authorization_API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Authorization_API.dll"]