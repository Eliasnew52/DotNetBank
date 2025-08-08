# 🏦 Bank API

A simple and RESTful API for managing **Clients** and their **Accounts** — built with ASP.NET Core, Entity Framework Core, and SQLite.

Done as Technical Assestment 

---

## 🚀 Features

- ✅ Create, read, update, and delete? (Ofc Not 💀) Clients and accounts  
- ✅ Retrieve all accounts for a specific client  
- ✅ Design Supports multiple account types (Debt, Credit)  
- ✅ JSON serialization with cycle handling  
- ✅ Interactive API docs powered by **Swagger UI**  
- ✅ Lightweight SQLite database for easy setup  
- ✅ Dependency Injection and clean architecture  

---
## 💻 Running the Project Locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQLite](https://sqlite.org/index.html) (optional, included via EF Core)
- A tool like [Postman](https://www.postman.com/downloads/) or curl to test the API

---

### Setup & Run

1. **Clone the repository**

```bash```
git clone https://github.com/Eliasnew52/DotNetBank.git
2. **Restore Dependencies**
cd DotNetBank
dotnet restore
3. **Apply Migrations**
dotnet ef database update
4. **Run the API**
dotnet run

The App will run on LocalHost:5010




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

The HOW 2 USE of the Endpoints is There thx to the Swagger XML Document Gen

