# ContactGate — Реестр пользователей и контактов

Тестовое задание для **KICB**. Веб-приложение для управления списком пользователей и их номерами телефонов.

## 🛠 Технологический стек
- **Backend:** .NET 10 (ASP.NET Core), Entity Framework Core.
- **Frontend:** Blazor WebAssembly, Bootstrap 5.
- **Database:** PostgreSQL.
- **Infrastructure:** Docker, Docker Compose.
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, WebAPI).
- **Validation:** FluentValidation (автоматическая валидация DTO).

## 🚀 Быстрый запуск
Для запуска всего окружения (БД + API + Клиент) необходимо иметь установленный Docker Desktop.

1. Склонируйте репозиторий.
2. В корне проекта выполните команду:
   ```bash
   docker-compose up --build

Доступ к сервисам:

    Frontend (Blazor): http://localhost:3000

    Backend API (Swagger): http://localhost:5000/swagger

    Database (PostgreSQL): порт 5444 (локально) / 5432 (внутри Docker).

💡 Особенности реализации

    Авто-миграции: При старте API автоматически проверяет состояние БД и применяет недостающие миграции (реализовано через context.Database.Migrate()).

    Middleware: Глобальная обработка исключений (Exception Handling Middleware) для возврата стандартизированных JSON-ответов при ошибках.

    CORS: Настроена политика доступа для корректного взаимодействия SPA-клиента с API.

    UX/Business Logic: - Динамическая фильтрация контактов по выбранному пользователю.

        Автоматическая нормализация номеров телефонов (очистка от лишних символов).

        Валидация данных на уровне Application с помощью FluentValidation.


---

### Финальные команды в терминале:

```bash
git add README.md
git commit -m "docs: финальное оформление документации проекта"
git push origin main