# \# Store Management API

# 

# A RESTful API for managing store products and categories, built with ASP.NET Core Web API and Clean Architecture.

# 

# \## Technologies Used

# 

# \- C#

# \- ASP.NET Core Web API

# \- SQL Server

# \- ADO.NET

# \- Stored Procedures

# \- MediatR

# \- Clean Architecture

# \- Swagger / OpenAPI

# 

# \## Architecture

# 

# The solution is organized into four projects:

# 

# \- \*\*Store.Api\*\* — API endpoints and dependency injection configuration.

# \- \*\*Store.Application\*\* — Application logic, commands, queries, and repository interfaces.

# \- \*\*Store.Domain\*\* — Core entities and domain models.

# \- \*\*Store.Infrastructure\*\* — SQL Server connection management and repository implementations.

# 

# \## Features

# 

# \- Create, retrieve, update, and delete categories.

# \- Create, retrieve, update, and delete products.

# \- Retrieve individual products and categories by ID.

# \- Use SQL Server stored procedures for database operations.

# \- Handle missing records with appropriate HTTP responses.

# \- Prevent deleting categories that contain products.

# 

# \## Database

# 

# The application uses SQL Server with two main entities:

# 

# \- \*\*Categories\*\*

# \- \*\*Products\*\*

# 

# Each product belongs to a category through a foreign key relationship.

# 

# \## Getting Started

# 

# 1\. Clone the repository.

# 2\. Create a SQL Server database named `StoreDB`.

# 3\. Create the required tables and stored procedures.

# 4\. Configure your local database connection string in `appsettings.json`.

# 5\. Restore NuGet packages and build the solution.

# 6\. Run `Store.Api` and open the Swagger UI to test the endpoints.

# 

# > The database connection string and other local configuration settings are not included in the repository. Configure them locally before running the application.

# 

# \## Purpose

# 

# This project was built to practice ASP.NET Core Web API development, ADO.NET, stored procedures, MediatR, dependency injection, and Clean Architecture.

