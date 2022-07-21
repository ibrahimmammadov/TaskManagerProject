#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["ProjectUI/ProjectUI.csproj", "ProjectUI/"]
#RUN dotnet restore "ProjectUI/ProjectUI.csproj"
#COPY . .
#WORKDIR "/src/ProjectUI"
#RUN dotnet build "ProjectUI.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "ProjectUI.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "ProjectUI.dll"]
#

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY ProjectUI/ProjectUI.csproj ProjectUI/ 
RUN dotnet restore  ProjectUI/ProjectUI.csproj
COPY . .
RUN dotnet publish ProjectUI/ProjectUI.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","ProjectUI.dll" ]