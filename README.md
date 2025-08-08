# 🏦 Bank API

A simple and RESTful API for managing **Clients** and their **Accounts** — built with ASP.NET Core, Entity Framework Core, and SQLite.

Done as Technical Assestment 

---

## 🚀 Features

- ✅ Create, read, update, and delete? (Ofc Not 💀) Clients and accounts  
- ✅ Retrieve all accounts for a specific client  
- ✅ Support for multiple account types (Debt, Credit)  
- ✅ JSON serialization with cycle handling  
- ✅ Interactive API docs powered by **Swagger UI**  
- ✅ Lightweight SQLite database for easy setup  
- ✅ Dependency Injection and clean architecture  

---

## 🛠️ Technologies Used

| Technology          | Description                           |
|---------------------|-------------------------------------|
| ![C#](https://img.shields.io/badge/-C%23-239120?logo=csharp&logoColor=white) | ASP.NET Core Web API framework      |
| ![EF Core](https://img.shields.io/badge/-Entity_Framework_Core-512BD4?logo=dotnet&logoColor=white) | ORM for database management         |
| ![SQLite](https://img.shields.io/badge/-SQLite-003B57?logo=sqlite&logoColor=white)       | Lightweight database engine         |
| ![Swagger](https://img.shields.io/badge/-Swagger-85EA2D?logo=swagger&logoColor=black)     | API documentation & testing UI      |
| ![Postman](https://img.shields.io/badge/-Postman-FF6C37?logo=postman&logoColor=white)     | API client for testing              |

---

## 📦 Example Endpoints

| Method | Endpoint                                 | Description                          |
| ------ | --------------------------------------- | ---------------------------------- |
| GET    | `/api/clients`                          | List all clients                   |
| GET    | `/api/clients/{clientId}`               | Get a client by ID                 |
| GET    | `/api/clients/{clientId}/accounts`     | List all accounts for a client     |
| POST   | `/api/clients/{clientId}/accounts`     | Create a new account for a client  |
| GET    | `/api/accounts/{accountId}`             | Get account by ID                  |
| POST   | `/api/clients/{clientId}/accounts/{accountId}/deposit`  | Deposit money into an account      |
| POST   | `/api/clients/{clientId}/accounts/{accountId}/withdraw` | Withdraw money from an account     |


## 📝 API Documentation [Swagger]
Once running, open your browser and navigate to:


- https://localhost:5010/swagger

