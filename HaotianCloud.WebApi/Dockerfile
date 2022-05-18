#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5006

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HaotianCloud.WebApi/HaotianCloud.WebApi.csproj", "HaotianCloud.WebApi/"]
COPY ["HaotianCloud.Service/HaotianCloud.Service.csproj", "HaotianCloud.Service/"]
COPY ["HaotianCloud.Data/HaotianCloud.DataBase.csproj", "HaotianCloud.Data/"]
COPY ["HaotianCloud.Code/HaotianCloud.Code.csproj", "HaotianCloud.Code/"]
COPY ["HaotianCloud.Domain/HaotianCloud.Domain.csproj", "HaotianCloud.Domain/"]
RUN dotnet restore "HaotianCloud.WebApi/HaotianCloud.WebApi.csproj"
COPY . .
WORKDIR "/src/HaotianCloud.WebApi"
RUN dotnet build "HaotianCloud.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HaotianCloud.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HaotianCloud.WebApi.dll"]