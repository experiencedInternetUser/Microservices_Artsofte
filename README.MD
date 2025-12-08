# TASK 3 

реализовал микросервис User Service с архитектурой Onion
с контролерами UsersController, RolesController, ProfilesController

Для его поднятия:

```
docker compose build user-api
docker compose up user-api
```

Затем открыть в браузере

http://localhost:5001/swagger/index.html


# Полное поднятие всего приложения

```
docker compose build
docker compose up
```

Затем открыть в браузере

http://localhost:5000/swagger/index.html