FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["UserService/User.Api/Api.csproj", "UserService/User.Api/"]
COPY ["UserService/User.Core/User.Core.csproj", "UserService/User.Core/"]
COPY ["UserService/User.Infrastructure/User.Infrastructure.csproj", "UserService/User.Infrastructure/"]
COPY ["UserService/User.Application/User.Application.csproj", "UserService/User.Application/"]

RUN dotnet restore "UserService/User.Api/Api.csproj"

COPY . .
WORKDIR /src/UserService/User.Api
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Api.dll"]
