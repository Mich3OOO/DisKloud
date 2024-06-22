#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DisKloud.Server/DisKloud.Server.csproj", "DisKloud.Server/"]
RUN dotnet restore "./DisKloud.Server/DisKloud.Server.csproj"
COPY . .
WORKDIR "/src/DisKloud.Server"
RUN dotnet build "./DisKloud.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# in the build stage we generate the cert
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG PASSWORD_ENV_SEEDED

#generate the cert, define the path to store it and password to use
RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p ${PASSWORD_ENV_SEEDED}

#then in final stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
COPY --chmod=0755 --from=build /https/* /https/


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DisKloud.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DisKloud.Server.dll"]