# GymPro – Project Overview

## Architecture

```text
Browser
  |
  v
GymPro.Web (Blazor Web App / Interactive Server)
  |  HttpClient
  v
GymPro.Api (ASP.NET Core Web API)
  |  Entity Framework Core 10
  v
SQL Server (GymManagementDB)
```

## Projects

- `GymPro.Web`: Blazor component UI, navigation, forms, tables, dashboard and API client.
- `GymPro.Api`: REST API controllers and application startup.
- `GymPro.Data`: Entity classes and EF Core DbContext.

## Main relationships

- One Member can have many Payments.
- Payments reference Members through `MemberId`.
- Trainers and Memberships are independent catalogue tables in this simple sample.

## API routes

| Method | Route | Purpose |
|---|---|---|
| GET | `/api/dashboard/summary` | Dashboard counters and recent members |
| GET | `/api/members` | List members; optional `?search=` |
| GET | `/api/members/{id}` | Get one member |
| POST | `/api/members` | Create member |
| PUT | `/api/members/{id}` | Update member |
| DELETE | `/api/members/{id}` | Delete member (blocked if payment history exists) |
| GET | `/api/trainers` | List trainers |
| GET | `/api/trainers/{id}` | Get one trainer |
| POST | `/api/trainers` | Create trainer |
| PUT | `/api/trainers/{id}` | Update trainer |
| DELETE | `/api/trainers/{id}` | Delete trainer |
| GET | `/api/memberships` | List plans |
| GET | `/api/memberships/{id}` | Get one plan |
| POST | `/api/memberships` | Create plan |
| PUT | `/api/memberships/{id}` | Update plan |
| DELETE | `/api/memberships/{id}` | Delete plan |
| GET | `/api/payments` | List payments including member details |
| GET | `/api/payments/{id}` | Get one payment |
| POST | `/api/payments` | Record payment |
| DELETE | `/api/payments/{id}` | Delete payment |

## UI pages

- `/` – Dashboard
- `/members` – Member list, search, add, edit and delete
- `/trainers` – Trainer list, add, edit and delete
- `/memberships` – Membership plan cards, add, edit and delete
- `/payments` – Payment history, record and delete

## Important sample limitations

- Membership plans are a catalogue. This sample does not yet assign a plan to a member or calculate subscription expiry.
- No login/authorization, trainer assignment, attendance, invoice PDF, or real payment gateway is included.
- Add authentication and authorization before exposing this application outside a trusted development environment.
