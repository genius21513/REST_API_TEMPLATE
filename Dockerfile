FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY *.csproj .
COPY . ./

RUN dotnet restore

RUN dotnet publish -c release -o /app

# db migration
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
#RUN dotnet ef migrations add Initial
#RUN dotnet ef database update

COPY migrate.exe /app/

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build  /app ./

EXPOSE 80
#EXPOSE 443

ENTRYPOINT ["dotnet", "REST_API_TEMPLATE.dll"]