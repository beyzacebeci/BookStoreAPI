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

#### GET `/api/books/{id}`

- Gets book details
- Public endpoint

#### GET `/api/books/search?title={title}`

- Searches books by title
- Case-insensitive, partial match supported
- Public endpoint

#### GET `/api/books/category/{categoryId}`

- Lists all books in a specific category
- Public endpoint

#### POST `/api/books`

```json
{
  "categoryId": 1,
  "title": "Sample Book",
  "author": "John Doe",
  "isbn": "1234567890",
  "price": 29.99,
  "stockQuantity": 100,
  "publicationYear": 2024
}
```

#### PUT `/api/books/{id}`

- Updates book information

#### DELETE `/api/books/{id}`

- Deletes a book

### 📦 Orders

#### GET `/api/orders/{id}`

- Gets order details with items

#### POST `/api/orders`

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

#### PUT `/api/orders/{id}/status`

```json
{
  "status": "COMPLETED"
}
```

Valid statuses: `PENDING`, `COMPLETED`, `CANCELLED`

### 🏷️ Categories

#### GET `/api/categories`

- Lists all categories

#### POST `/api/categories`

- Creates new category

## 📊 Database Schema

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

### Status Codes

- 200: Success
- 400: Bad Request
- 404: Not Found
- 500: Server Error

## 📦 Dependencies

- Microsoft.EntityFrameworkCore
- FluentValidation
- Npgsql.EntityFrameworkCore.PostgreSQL

## 🤝 Contributing

1. Fork the repository
2. Create feature branch
3. Commit changes
4. Push to branch
5. Create Pull Request
