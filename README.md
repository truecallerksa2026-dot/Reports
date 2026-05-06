# ReportBuilder

Dynamic report builder built on ABP Commercial (Angular + .NET 10 + PostgreSQL).

---

## Run with Docker (recommended)

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) installed and running

### 1 — Add the local domain to your hosts file

**Windows** — open Notepad as Administrator, edit `C:\Windows\System32\drivers\etc\hosts`:
```
127.0.0.1  dev-reports.local
```

**Mac / Linux:**
```bash
sudo sh -c 'echo "127.0.0.1 dev-reports.local" >> /etc/hosts'
```

### 2 — Clone and start

```bash
git clone https://github.com/AhmedSasko/ReportBuilder.git
cd ReportBuilder
docker compose -f docker-compose.dev-reports.yml up --build
```

First run takes ~5 minutes (builds .NET + Angular from source). Subsequent starts are fast.

### 3 — Open the app

```
http://dev-reports.local
```

| Field    | Value     |
|----------|-----------|
| Username | `admin`   |
| Password | `1q2w3E*` |

> **That is it. No other configuration needed.**

---

## Run locally without Docker

### Prerequisites
- .NET 10 SDK
- Node.js 20 + Yarn
- PostgreSQL 16 running on localhost:5432

Spin up just the database with Docker:
```bash
docker compose up -d
```

### Steps

```bash
# 1 — Migrate and seed
dotnet run --project src/ReportBuilder.DbMigrator

# 2 — Start the API  (https://localhost:44356)
dotnet run --project src/ReportBuilder.HttpApi.Host

# 3 — Start Angular  (http://localhost:4200)
cd angular && yarn install && yarn start
```

---

## Architecture

```
nginx (port 80, dev-reports.local)
  ├── /api, /connect, /Account ...  →  .NET 10 API (ABP + OpenIddict + EF Core)
  └── /*                            →  Angular SPA (LeptonX theme)

PostgreSQL 16
  ├── rw_user  — read/write (API writes)
  └── ro_user  — read-only  (report query execution)
```

## Features

- **Report definitions** — define SQL queries with `@paramName` parameters
- **Column editor** — visibility, filterability, sortability, grouping per column
- **Column role permissions** — show/hide columns per role with expand arrow (▶) in the Columns tab
- **Report permissions** — restrict entire reports to specific roles
- **Grid view** — DevExtreme DataGrid with server-side paging and export
- **Report view** — clean printable table with Load More support
- **Student module** — sample data with 100 seeded student records
