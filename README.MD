Task Service реализован с помощью трехслойной архитекруты (API, DAL, BL)

Реализованы Projects и Labels CRUD.
Tasks - CRUD содержит несколько сущностей (при создании/хранении учитываются Assignments и TaskLabels)


# Быстрый старт:

docker compose build
docker compose up

Затем открыть в браузере
http://localhost:5000/swagger/index.html