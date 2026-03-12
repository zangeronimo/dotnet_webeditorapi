# WebEditor API

A clean architecture API built with **.NET 10**, designed to manage entities such as **Company**, **User**, **Role**, and **Module**.  
The project emphasizes separation of concerns, dependency injection, and maintainability, while leveraging Entity Framework Core and PostgreSQL.

---

## 🚀 Tech Stack

- [.NET 10](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/core/) for data access
- [PostgreSQL](https://www.postgresql.org/) as the database
- [Minimal APIs](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis) for lightweight endpoints
- [DotNetEnv](https://github.com/tonerdo/dotnet-env) for environment variable management

---

## 🏗️ Project Structure

dotnet_webeditorapi/
├── src/
│   ├── WEBEditorAPI.Domain/          # Entities and contracts (repository interfaces)
│   ├── WEBEditorAPI.Application/     # Services and use cases (business logic)
│   ├── WEBEditorAPI.Infrastructure/  # Infrastructure (EF Core, DbContext, repositories, DI)
│   └── WEBEditorAPI.API/             # Minimal API endpoints
└── tests/                            # Unit and integration tests

### 📌 Layers

- **Domain**  
  - Core entities (`Company`, `User`, etc.)  
  - Generic repository contract (`IRepository<T>`)  
  - Entity-specific contracts (`ICompanyRepository`, `IUserRepository`, etc.)  

- **Application**  
  - Services (`CompanyService`)  
  - Orchestrates business rules and use cases  

- **Infrastructure**  
  - `AppDbContext` and entity mappings  
  - Repository implementations (`CompanyRepository`)  
  - Dependency injection registration (`DependencyInjection`)  

- **API**  
  - Minimal API endpoints (`/companies`, etc.)  
  - Consumes Application services via DI  

---

## 📜 License
This project is licensed under the MIT License.

## 👤 Author

**Name:** Luciano Zangeronimo  
**Email:** [zangeronimo@gmail.com](mailto:zangeronimo@gmail.com)