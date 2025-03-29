# Inventory Management API

## Overview
The Inventory Management API is a .NET 8 application that provides endpoints for managing products in an inventory. The application follows the Command Query Responsibility Segregation (CQRS) pattern and uses MediatR for handling commands and queries.

## Solution Structure
The solution is organized into several projects:

- **InventoryManagement.Api**: Contains the API controllers and configuration.
- **InventoryManagement.Service**: Contains the business logic, MediatR handlers, and DTOs.
- **InventoryManagement.Repository**: Contains the data access layer and entity models.
- **InventoryManagement.Test**: Contains unit tests for the application.

## Projects

### InventoryManagement.Api
- **Controllers**: Contains the API controllers that handle HTTP requests.
- **Configuration**: Contains the configuration classes for setting up services and dependencies.

### InventoryManagement.Service
- **Products**: Contains the business logic for managing products, including MediatR handlers, commands, and queries.
- **Dto**: Contains the Data Transfer Objects (DTOs) used for communication between layers.

### InventoryManagement.Repository
- **Model**: Contains the entity models representing the database tables.
- **Products**: Contains the repository interfaces and implementations for managing products.

### InventoryManagement.Test
- **Controllers**: Contains unit tests for the API controllers.
- **Services**: Contains unit tests for the MediatR handlers.
- **Repositories**: Contains unit tests for the repository classes.

## Running the Project

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or any other IDE that supports .NET development

### Steps to Run
1. **Clone the repository**:

2. **Build the solution**:
   Open the solution in Visual Studio and build the solution, or use the .NET CLI: 

3. **Run the application**:
   You can run the application using Visual Studio or the .NET CLI:
   
4. **Run the tests**:
   You can run the tests using Visual Studio Test Explorer or the .NET CLI:
   
### Configuration
The application uses an in-memory SQLite database for development and testing. The database schema is created and migrations are applied at runtime.

### API Endpoints
The following endpoints are available:

- **GET /products**: Returns all products.
- **GET /products/{id}**: Returns a product by ID.
- **POST /products**: Inserts a new product.
- **PUT /products**: Updates an existing product.
- **DELETE /products/{id}**: Deletes a product by ID.

### Example Requests
- **GET /products**:
  
- **POST /products**:
  
## Conclusion
This README provides an overview of the Inventory Management API, its structure, and instructions on how to run the project. The application follows the CQRS pattern and uses MediatR for handling commands and queries, ensuring a clean separation of concerns and maintainable code.
