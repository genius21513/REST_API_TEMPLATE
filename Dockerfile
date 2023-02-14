# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
# COPY REST_API_TEMPLATE/*.sln .
COPY REST_API_TEMPLATE/*.csproj .
RUN dotnet restore --use-current-runtime

# copy everything else and build app
COPY REST_API_TEMPLATE/. .
WORKDIR /source/REST_API_TEMPLATE
RUN dotnet publish -c release -o /app --use-current-runtime --self-contained false --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "REST_API_TEMPLATE.dll"]
