## Чтобы запустить локально:
1. Запустить PostgreSQL на порту 1111 для этого использовать:
```
docker-compose up -d
```
2. Обновить базу данных используя миграции:
```
dotnet ef database update --project Notes.Infrastructure --startup-project Notes.Api
```
3. Если пункт №2 не сработал:
```
dotnet tool install --global dotnet-ef
```
повторить пункт №2

4. Запустить приложение через `dotnet run --project Notes.Api` или через IDE

Чтобы запустить тесты 
```
dotnet test
```
Примеры запросов лежат в файле **Notes.postman_collection.json**