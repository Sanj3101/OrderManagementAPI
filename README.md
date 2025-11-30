# Order Management API (ASP.NET Core 8 + Identity + JWT + SQL Server)

screenshot requirements at : [Click here to view all screenshots](./Assignment3_Screenshots.pdf)

## Project Structure

```text
OrderMgmtAPI/
│── Controllers/
│   ├── AuthController.cs
│   └── OrdersController.cs
│
│── Data/
│   └── ApplicationDbContext.cs
│
│── Models/
│   ├── ApplicationUser.cs
│   ├── Order.cs
│   └── DTOs/
│       ├── LoginDto.cs
│       ├── RegisterDto.cs
│       └── OrderDto.cs
│
│── Migrations/
│── Program.cs
│── appsettings.json
```
## Authentication Endpoints

1. Register a new user

POST → ```/api/auth/register```
Body:
```
{
  "email": "user@example.com",
  "password": "Pass@123"
}
```
2. Login to get JWT token

POST → ```/api/auth/login```
Body:
```
{
  "email": "user@example.com",
  "password": "Pass@123"
}
```

Response:
```
{
  "token": "eyJhbGc..."
}
```

## Order Endpoints (Protected)

- POST	```/api/orders``` : 	Create a new order
- GET	```/api/orders```	  :   Get all orders for logged-in user
- GET	```/api/orders/{id}``` :	Get a single order
- PUT	```/api/orders/{id}``` :	Update order
- DELETE	```/api/orders/{id}``` :	Delete order
