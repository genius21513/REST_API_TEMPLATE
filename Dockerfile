FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY *.csproj ./
COPY . ./

RUN dotnet restore

RUN dotnet publish -c release -o /app

#COPY migrate.exe /app

# db migration with sdk:6.0
#RUN dotnet tool install --global dotnet-ef
#ENV PATH="${PATH}:/root/.dotnet/tools"
#RUN dotnet ef migrations add Initial
#RUN dotnet ef database update


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build  /app ./

ENV ASPNETCORE_ENVIRONMENT Docker

EXPOSE 80
#EXPOSE 443

ENTRYPOINT ["dotnet", "REST_API_TEMPLATE.dll"]