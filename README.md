# Curnexa — Personal Finance Web API

Curnexa is a personal finance and multi-currency management API built with ASP.NET Core. It provides endpoints for managing wallets, transactions, categories, currencies, exchange rates, and currency conversions.

## Features

- **Authentication:** User registration and login using ASP.NET Core Identity and JWT authentication.
- **Wallet management:** Create and manage wallets.
- **Transactions:** Manage income and expenses.
- **Categories:** Organize transactions by category.
- **Multi-currency support:** Manage currencies and exchange rates.
- **Currency conversion:** Convert amounts between currencies.
- **Dashboard:** Retrieve a summary of balances, income, expenses, wallets, and recent transactions.
- **API documentation:** Explore and test endpoints using Swagger UI.

## Technology Stack

- C#
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- Swagger / OpenAPI

## Architecture

The API uses a controller-service-data access structure:

`Controller → Service → DbContext`

Controllers handle HTTP requests, services contain application logic, and Entity Framework Core interacts with the database.

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server
- Visual Studio 2022 or another compatible .NET IDE

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/victoria66-coder/Curnexa.git
   cd Curnexa
   ```

2. Configure your local database connection string and JWT signing key in `appsettings.Development.json`. Never commit real secrets to source control.

3. Apply the Entity Framework Core migrations using the appropriate startup project and environment configuration.

4. Run the API:

   ```bash
   dotnet run
   ```

5. Open the Swagger URL printed in the terminal to explore the API endpoints.

## Configuration and Security

Local development settings are excluded from Git through `.gitignore`. Supply production database credentials and JWT signing keys through secure environment variables or your hosting provider's application settings.

## Project Status

The backend is being prepared for deployment. A public repository does not necessarily mean the API itself is deployed or publicly accessible yet.

## Author

**Victoria Olayera**

- GitHub: [victoria66-coder](https://github.com/victoria66-coder)
