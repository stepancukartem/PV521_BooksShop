PV521_BooksShop — AuthorRepository
=================================

Це готовий ASP.NET Core Web API проєкт під завдання "Написати AuthorRepository".

СТРУКТУРА
---------
PV521_BooksShop.sln
  ├─ PV521_BookssShop       - Web API
  ├─ PV521_BooksShop.DAL    - Entity Framework + PostgreSQL + Repository
  └─ PV521_BooksShop.BLL    - BLL

ГОЛОВНИЙ ФАЙЛ ЗАВДАННЯ
----------------------
PV521_BooksShop.DAL/Repositories/AuthorRepostiory.cs

У ньому реалізовані:
- Authors
- GetByIdAsync
- CreateAsync
- CreateRangeAsync
- UpdateAsync
- DeleteAsync(Author)
- DeleteAsync(int)

ЩО ТРЕБА ЗМІНИТИ ПІСЛЯ РОЗПАКУВАННЯ
------------------------------------
Відкрий:
PV521_BookssShop/appsettings.json

Заміни:
YOUR_POSTGRES_PASSWORD

на свій пароль PostgreSQL.

База даних:
pv521_api
Host:
localhost
Port:
5432

ПЕРЕД ЗАПУСКОМ
--------------
1. Відкрий PV521_BooksShop.sln у Visual Studio.
2. Restore NuGet Packages.
3. Build -> Rebuild Solution.
4. Переконайся, що PostgreSQL запущений.
5. У Package Manager Console вибери Default project:
   PV521_BooksShop.DAL
6. Startup Project постав:
   PV521_BookssShop
7. Створи міграцію:
   Add-Migration InitialCreate
8. Застосуй її:
   Update-Database
9. Запусти Web API.

API ДЛЯ ПЕРЕВІРКИ
-----------------
GET    /api/authors
GET    /api/authors/1
POST   /api/authors
PUT    /api/authors/1
DELETE /api/authors/1

Приклад POST:
{
  "name": "J. K. Rowling",
  "biography": "Author biography",
  "country": "United Kingdom",
  "birthDate": "1965-07-31T00:00:00Z"
}

ВАЖЛИВО
-------
Назва AuthorRepostiory написана з тією ж помилкою "Repostiory",
що й BookRepostiory у вихідному навчальному репозиторії.
Якщо викладач вимагає саме AuthorRepository без помилки,
перейменуй файл/клас і всі посилання з Repostiory на Repository.
