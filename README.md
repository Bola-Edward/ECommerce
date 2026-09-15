# ECommerce

ASP.NET Core e-commerce API with JWT authentication, email verification (teaching NoOp), refresh tokens, product catalog, basket management, orders, payments, and user addresses.

Built with a clean layered architecture and **two EF Core DbContexts** sharing one SQL Server database (Identity vs business data).

## Solution structure

| Project | Role |
|---------|------|
| `ECommerce.API` | Web API, Swagger, authentication, DI composition |
| `ECommerce.UseCases` | Commands, queries, handlers, validation, and application logic |
| `ECommerce.Domain` | Entities, business rules, errors, and Result pattern |
| `ECommerce.Infrastructure` | EF Core, Identity, JWT, caching, external services, and seeding |
| `ECommerce.ArchitectureTests` | Clean Architecture dependency tests |

## ✨ Key Features

- Clean Architecture with CQRS and Vertical Slice organization
- Product catalog with filtering, sorting, pagination, and specifications
- Basket management with HybridCache and optional Redis
- Order and delivery management
- Stripe payments and webhook handling
- JWT authentication with refresh tokens
- Email verification
- Cloudinary product image storage
- Result Pattern for expected operation failures
- Audit tracking and soft delete
- Architecture tests with NetArchTest

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB / full instance) — default connection: `Server=.;Database=ECommerceDb;...`
- Optional: Redis for distributed HybridCache (`ConnectionStrings:redis`)

## Quick start

```bash
dotnet restore
dotnet run --project src/ECommerce.API
```

In Development the API will:

1. Apply Identity + Application migrations  
2. Seed roles, super-admin, brands/types  
3. Open Swagger UI  

Swagger: `https://localhost:<port>/swagger`

### Seeded super-admin

Configured in `appsettings.Development.json` under `Seed:SuperAdmin` (change the password for anything beyond local use).

## Authentication flow

1. **Register** — `POST /api/auth/register`  
   Creates an unconfirmed user and stores a verification code (email is **not** sent yet).
2. **Copy the code from the console** — `NoOpEmailSender` logs it.
3. **Confirm email** — `POST /api/auth/confirm-email` → access + refresh tokens.
4. **Login** — `POST /api/auth/login` (confirmed accounts only).
5. **Refresh** — `POST /api/auth/refresh` (rotates refresh token).
6. **Logout** — `POST /api/auth/logout` (revokes refresh token).

Use the access token as:

```http
Authorization: Bearer {accessToken}
```

### Basket buyer id

- **Guest:** send header `X-Buyer-Id: {client-generated-guid}`
- **Authenticated:** buyer id comes from the JWT (`NameIdentifier`); do not trust a spoofed header

After login, merge the guest basket with `POST /api/basket/merge`.

## Main API Routes

| Area | Available Operations |
|------|----------------------|
| **Authentication** | Register, email confirmation, login, refresh tokens, logout |
| **Users** | View and update profile, manage addresses |
| **Catalog** | Products, brands, and product types |
| **Basket** | View, add, update, remove, clear, and merge basket |
| **Orders** | Checkout, order history, order details, cancellation |
| **Delivery** | List, create, update, and delete delivery methods |
| **Payments** | Stripe PaymentIntent creation and webhook handling |

## Architecture notes

## Architecture notes

- **Clean Architecture** with dependencies flowing toward the Domain layer.
- **CQRS** with feature-based / vertical slice organization.
- **Result Pattern** for expected business failures.
- **Specification Pattern** for reusable filtering, sorting, pagination, and projection.
- **Generic Repository + Unit of Work** abstractions defined in the Domain layer and implemented in Infrastructure.
- Two EF Core DbContexts sharing one SQL Server database:
  - `ECommerceIdentityDbContext` → Identity data and refresh tokens
  - `ECommerceDbContext` → application/business data
- UseCases return `Result` / `Result<T>` (no exception-driven flow for expected failures).
- - JWT access tokens are short-lived; refresh tokens are opaque, hashed in the DB, and rotated on use.

## Configuration

Important sections in `appsettings` / `appsettings.Development.json`:

- `ConnectionStrings:DefaultConnection`
- `Jwt` — `Secret` (≥ 32 chars), issuer, audience, access/refresh lifetimes
- `EmailVerification` — code length, expiry, max attempts
- `Seed:SuperAdmin`
- `CachedAggregates:Basket`
- `CloudinarySettings` — product image uploads (use your own keys)
- `StripeSettings` — Stripe API and webhook configuration

## Migrations

```bash
# Identity
dotnet ef migrations add <Name> --context AppIdentityDbContext --output-dir Migrations/Identity --project src/ECommerce.Infrastructure --startup-project src/ECommerce.API

# Application
dotnet ef migrations add <Name> --context ApplicationDbContext --output-dir Migrations/Application --project src/ECommerce.Infrastructure --startup-project src/ECommerce.API
```


