FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["TaskService/Task.Api/Api.csproj", "TaskService/Task.Api/"]
COPY ["TaskService/Task.Core/CoreLib.csproj", "TaskService/Task.Core/"]
COPY ["TaskService/Task.Dal/Dal.csproj", "TaskService/Task.Dal/"]
COPY ["TaskService/Task.Logic/Logic.csproj", "TaskService/Task.Logic/"]

RUN dotnet restore "TaskService/Task.Api/Api.csproj"

COPY . .

WORKDIR /src/TaskService/Task.Api
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Api.dll"]
