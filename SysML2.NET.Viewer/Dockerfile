FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY Nuget.Config .
COPY SysML2.NET SysML2.NET
COPY SysML2.NET.Serializer.Json SysML2.NET.Serializer.Json
COPY SySML2.NET.REST SySML2.NET.REST
COPY SysML2.NET.Viewer SysML2.NET.Viewer
RUN dotnet restore --configfile Nuget.Config SysML2.NET.Viewer
RUN dotnet build SysML2.NET.Viewer -c Release -o /app/build

FROM build AS publish
RUN dotnet publish SysML2.NET.Viewer -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY SysML2.NET.Viewer/nginx.conf /etc/nginx/nginx.conf