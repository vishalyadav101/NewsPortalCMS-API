
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["NewsPortalCMS/NewsPortalCMS.csproj", "NewsPortalCMS/"]
COPY ["NewsPortalCMS.Application/NewsPortalCMS.Application.csproj", "NewsPortalCMS.Application/"]
COPY ["NewsPortalCMS.Domain/NewsPortalCMS.Domain.csproj", "NewsPortalCMS.Domain/"]
COPY ["NewsPortalCMS.Infrastructure/NewsPortalCMS.Infrastructure.csproj", "NewsPortalCMS.Infrastructure/"]
COPY ["NewsPortalCMS.Shared/NewsPortalCMS.Shared.csproj", "NewsPortalCMS.Shared/"]

RUN dotnet restore "NewsPortalCMS/NewsPortalCMS.csproj"

COPY . .
WORKDIR /src/NewsPortalCMS
RUN dotnet publish "NewsPortalCMS.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "NewsPortalCMS.dll"]
