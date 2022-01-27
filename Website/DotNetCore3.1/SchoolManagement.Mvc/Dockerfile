#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SchoolManagement.Mvc/SchoolManagement.Mvc.csproj", "SchoolManagement.Mvc/"]
COPY ["SchoolManagement.Application/SchoolManagement.Application.csproj", "SchoolManagement.Application/"]
COPY ["SchoolManagement.Core/SchoolManagement.Core.csproj", "SchoolManagement.Core/"]
COPY ["SchoolManagement.EntityFrameworkCore/SchoolManagement.EntityFrameworkCore.csproj", "SchoolManagement.EntityFrameworkCore/"]
RUN dotnet restore "SchoolManagement.Mvc/SchoolManagement.Mvc.csproj"
COPY . .
WORKDIR "/src/SchoolManagement.Mvc"
RUN dotnet build "SchoolManagement.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SchoolManagement.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SchoolManagement.Mvc.dll"]