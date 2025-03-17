# 📚 BookStore API Documentation

A simple RESTful API for managing a bookstore's inventory and orders.

## 🚀 Features

- **Book Management**

  - CRUD operations for books
  - Search books by title
  - Category-based book organization
  - Stock management

- **Order Management**

  - Create orders
  - Track order status
  - View order details
  - Stock validation
  - Order history

- **Category Management**
  - CRUD operations for categories
  - Category-book relationships

## 🛠 Technical Stack

- **.NET 9.0**
- **Entity Framework Core**
- **PostgreSQL**
- **FluentValidation**
- **AutoMapper**
- **Swagger/OpenAPI**

## 🏗 Architecture

The project follows Clean Architecture principles:

- **App.API** - Controllers, API Endpoints
- **App.Services** - Business Logic, DTOs, Validation
- **App.Repositories** - Entities, Interfaces
  ,Data Access, Entity Configurations

## 🔧 Setup

### 1. Clone Repository

```bash
git clone https://github.com/beyzacebeci/BookStoreAPI
```

### 2. Update Connection String

In `appsettings.json`:

```json
"ConnectionStrings": {
    "PostgreSQL": "Host=localhost;Database=BookStoreDb;Username=your_username;Password=your_password;"
}
```

### 3. Database Migration Steps

### Using Visual Studio Package Manager Console

1. Set App.API as startup project
2. Open Package Manager Console
3. Set App.Repositories as Default Project in PMC
4. Run these commands:

```bash
# Create initial migration
Add-Migration InitialCreate

# Apply migration to database
Update-Database
```

### Using .NET CLI

1. Open terminal in solution directory
2. Navigate to App.Repositories project:

```bash
cd App.Repositories
```

3. Create migration:

```bash
dotnet ef migrations add InitialCreate --startup-project ../App.API
```

4. Apply migration:

```bash
dotnet ef database update --startup-project ../App.API
```

## 📝 API Endpoints

### 📚 Books

#### GET `/api/books`

- Lists all books
- Public endpoint
- Returns:

```json
{
  "data": [
    {
      "id": 1,
      "categoryId": 1,
      "title": "Suç ve Ceza",
      "author": "Fyodor Dostoyevski",
      "isbn": "9789750719387",
      "price": 49.99,
      "stockQuantity": 0,
      "publicationYear": 1866
    }
  ],
  "errorMessage": null,
  "httpStatusCode": 200,
  "isSuccess": true
}
```

#### GET `/api/books/{id}`

- Gets book details by ID
- Public endpoint
- Returns:

```json
{
  "data": {
    "id": 1,
    "categoryId": 1,
    "title": "Suç ve Ceza",
    "author": "Fyodor Dostoyevski",
    "isbn": "9789750719387",
    "price": 49.99,
    "stockQuantity": 0,
    "publicationYear": 1866
  },
  "errorMessage": null,
  "httpStatusCode": 200,
  "isSuccess": true
}
```

#### GET `/api/books/search?title={title}`

- Searches books by title
- Case-insensitive, partial match supported
- Public endpoint

```json
{
  "data": [
    {
      "id": 3,
      "categoryId": 3,
      "title": "Nutuk",
      "author": "Mustafa Kemal Atatürk",
      "isbn": "9789944885348",
      "price": 69.99,
      "stockQuantity": 40,
      "publicationYear": 1927
    }
  ],
  "errorMessage": null,
  "httpStatusCode": 200,
  "isSuccess": true
}
```

#### GET `/api/books/category/{categoryId}`

- Lists all books in a specific category
- Public endpoint

#### POST `/api/books`

- Create a book
- Request body:

```json
{
  "categoryId": 2,
  "title": "Beyaz Gemi",
  "author": "Cengiz Aytmatov",
  "isbn": "4353453445",
  "price": 55,
  "stockQuantity": 45,
  "publicationYear": 1965
}
```

- Returns:

```json
{
  "data": {
    "id": 8
  },
  "errorMessage": null,
  "httpStatusCode": 201,
  "isSuccess": true
}
```

#### PUT `/api/books/{id}`

- Updates book information
- Request body:

```json
{
  "categoryId": 1,
  "title": "Updated Book Title",
  "author": "John Doe",
  "isbn": "1234567890",
  "price": 29.99,
  "stockQuantity": 100,
  "publicationYear": 2024
}
```

- Returns:

```json
{
  "errorMessage": null,
  "httpStatusCode": 204
}
```

#### DELETE `/api/books/{id}`

- Deletes a book
- Returns:

```json
{
  "errorMessage": null,
  "httpStatusCode": 204
}
```

### 📦 Orders

#### GET `/api/orders/{id}`

- Gets order details with items
- Returns:

```json
{
  "isSuccessful": true,
  "data": {
    "id": 1,
    "orderDate": "2024-03-16T10:00:00Z",
    "totalPrice": 59.98,
    "status": "PENDING",
    "orderItems": [
      {
        "bookId": 1,
        "quantity": 2,
        "unitPrice": 29.99
      }
    ]
  },
  "errors": null,
  "httpStatusCode": 200
}
```

#### POST `/api/orders`

- Creates a new order
- Validates stock availability
- Updates book stock quantities
- Request body:

```json
{
  "orderItems": [
    {
      "bookId": 1,
      "quantity": 2
    }
  ]
}
```

- Returns:

```json
{
  "isSuccessful": true,
  "data": {
    "id": 1,
    "totalPrice": 59.98
  },
  "errors": null,
  "httpStatusCode": 201
}
```

#### PUT `/api/orders/{id}/status`

- Updates order status
- Cannot update COMPLETED orders
- Restores book stock quantities when cancelled
- Request body:

```json
{
  "status": "COMPLETED"
}
```

- Returns:

```json
{
  "errorMessage": null,
  "httpStatusCode": 200
}
```

### 🏷️ Categories

#### GET `/api/categories`

- Lists all categories
- Returns:

```json
{
  "data": [
    {
      "id": 1,
      "name": "Nobel"
    },
    {
      "id": 2,
      "name": "Science"
    }
  ],
  "errorMessage": null,
  "httpStatusCode": 200,
  "isSuccess": true
}
```

#### POST `/api/categories`

- Creates new category
- Request body:

```json
{
  "name": "Fiction"
}
```

- Returns:

```json
{
  "data": {
    "id": 5
  },
  "errorMessage": null,
  "httpStatusCode": 201,
  "isSuccess": true
}
```

## 📊 Database Schema

![Ekran görüntüsü 2025-03-16 015332](https://github.com/user-attachments/assets/6fd442cb-54df-483c-b589-e878fe9aee4b)

### Books

```sql
- Id (PK)
- CategoryId (FK)
- Title
- Author
- ISBN
- Price
- StockQuantity
- PublicationYear
```

### Orders

```sql
- Id (PK)
- OrderDate
- TotalPrice
- Status
```

### OrderItems

```sql
- Id (PK)
- OrderId (FK)
- BookId (FK)
- Quantity
- UnitPrice
```

### Categories

```sql
- Id (PK)
- Name
```

## 🔒 Error Handling

All endpoints return standardized response:

```json
{
  "isSuccessful": true,
  "data": {},
  "errors": null,
  "httpStatusCode": 200
}
```

Example error response:

```json
{
  "isSuccessful": false,
  "data": null,
  "errors": ["Book not found"],
  "httpStatusCode": 404
}
```

### Status Codes

- 200: Success
- 201: Created
- 204: No Content
- 400: Bad Request
- 404: Not Found
- 500: Server Error

## 📦 Dependencies

- Microsoft.EntityFrameworkCore
- FluentValidation
- Npgsql.EntityFrameworkCore.PostgreSQL
- AutoMapper

## 🤝 Contributing

1. Fork the repository
2. Create feature branch
3. Commit changes
4. Push to branch
5. Create Pull Request

## 📋 Requirements

- .NET 9.0 SDK
- PostgreSQL 15 or higher
