FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["TaskService/Api/Api.csproj", "TaskService/Api/"]
COPY ["TaskService/CoreLib/CoreLib.csproj", "TaskService/CoreLib/"]
COPY ["TaskService/Dal/Dal.csproj", "TaskService/Dal/"]
COPY ["TaskService/Logic/Logic.csproj", "TaskService/Logic/"]

RUN dotnet restore "TaskService/Api/Api.csproj"

COPY . .
WORKDIR /src/TaskService/Api
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Api.dll"]
