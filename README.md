# AuthService

AuthService is a lightweight authentication microservice built with .NET 9 using Minimal API and Clean Architecture principles. It provides JWT-based authentication endpoints that can be reused across multiple applications.

## 🔐 Features

- User login with JWT token generation
- Token validation and expiration handling
- Clean Architecture structure:
  - Domain
  - Application
  - Infra.Data
  - Infra.IoC
  - WebAPI
  - Tests
- Minimal API for fast and clean endpoint definitions
- Ready for integration with other services via HTTP

## 📦 Technologies

- .NET 9
- ASP.NET Core Minimal API
- JWT (JSON Web Token)
- Entity Framework Core
- Clean Architecture
- Swagger (OpenAPI)

## 🚀 Endpoints

| Method | Route         | Description             |
|--------|---------------|-------------------------|
| POST   | `/login`      | Authenticates user and returns JWT |
| POST   | `/register`   | Creates a new user account |
| GET    | `/validate`   | Validates a JWT token   |

## 🧠 About JWT

JWT (JSON Web Token) is a compact, URL-safe token format used for securely transmitting information between parties. It consists of three parts:

1. **Header**: Specifies the signing algorithm (e.g., HS256)
2. **Payload**: Contains claims (user ID, roles, etc.)
3. **Signature**: Verifies the token's integrity

Tokens are signed using a secret key and can be validated without storing session state on the server.

## 🛠 Setup

```bash
dotnet restore
dotnet build
dotnet run --project AuthService.WebAPI
