# TASK 4

TaskService теперь асинхронно вызывает UserService

В TaskService/Task.Logic/HTTP реализовал HttpService
В TaskService/Task.Logic/Trace реализовал TraceId

В UserService/User.Api/Trace реализовал scoped-доступ к TraceId

# Полное поднятие всего приложения

```
docker compose build
docker compose up
```

Затем открыть в браузере

Энд-поинты TaskService: http://localhost:5000/swagger/index.html
Энд-поинты UserService: http://localhost:5001/swagger/index.html