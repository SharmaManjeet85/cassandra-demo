# Photo App — .NET 10 + Cassandra + Aspire + React

A production-style distributed photo application built using:

- .NET 10 Web API
- Apache Cassandra
- .NET Aspire
- React Frontend (planned)
- Serilog Structured Logging
- Correlation IDs
- Health Checks
- FluentValidation
- OpenTelemetry-ready architecture

---

# Architecture

```text
┌────────────────────┐
│     React UI       │
└─────────┬──────────┘
          │ HTTP
          ▼
┌────────────────────┐
│     Photo.Api      │
│  ASP.NET Core API  │
└─────────┬──────────┘
          │ CQL
          ▼
┌────────────────────┐
│     Cassandra      │
│ Distributed NoSQL  │
└────────────────────┘
```

---

# Current Features

## API

- Create Photo
- Get Photo By Id
- Get Photos By User
- Cassandra integration
- Repository pattern
- Service layer
- DTO validation
- Global exception middleware

---

## Observability

- Serilog structured logging
- Correlation IDs
- Request tracing
- Health checks
- Startup validation

---

## Distributed App

- .NET Aspire AppHost
- Cassandra container orchestration
- Centralized dashboard
- Service lifecycle management

---

# Solution Structure

```text
photo-app/
│
├── Photo.Api
│   ├── Controllers
│   ├── Middlewares
│   ├── HostedServices
│   ├── Extensions
│   └── Program.cs
│
├── Photo.Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Validators
│
├── Photo.Domain
│   └── Entities
│
├── Photo.Infrastructure
│   └── Repositories
│
├── Photo.AppHost
│
└── Photo.ServiceDefaults
```

---

# Technologies Used

| Technology | Purpose |
|---|---|
| ASP.NET Core | REST API |
| Cassandra | Distributed database |
| .NET Aspire | Distributed orchestration |
| Serilog | Structured logging |
| FluentValidation | Request validation |
| Docker | Cassandra container |
| OpenTelemetry | Future tracing |
| React | Frontend UI |

---

# Prerequisites

Install:

- .NET 10 SDK
- Docker Desktop
- Visual Studio 2022 / VS Code
- Aspire workload

---

# Install Aspire Workload

```bash
dotnet workload install aspire
```

---

# Run Cassandra Manually (Optional)

```bash
docker run --name cassandra-db ^
-p 9042:9042 ^
-d cassandra
```

---

# Run Using Aspire

From solution root:

```bash
dotnet run --project Photo.AppHost
```

---

# Aspire Dashboard

Aspire automatically opens dashboard in browser.

Dashboard provides:

- API logs
- health status
- traces
- container state
- endpoints

---

# Configuration

## appsettings.json

```json
{
  "Cassandra": {
    "ContactPoints": [ "localhost" ],
    "Keyspace": "photo_app",
    "Datacenter": "datacenter1"
  }
}
```

---

# Cassandra Tables

## photos_by_id

```sql
CREATE TABLE IF NOT EXISTS photos_by_id
(
    photo_id UUID PRIMARY KEY,
    user_id TEXT,
    title TEXT,
    url TEXT,
    uploaded_at TIMESTAMP
);
```

---

## photos_by_user

```sql
CREATE TABLE IF NOT EXISTS photos_by_user
(
    user_id TEXT,
    uploaded_at TIMESTAMP,
    photo_id UUID,
    title TEXT,
    url TEXT,
    PRIMARY KEY (user_id, uploaded_at)
)
WITH CLUSTERING ORDER BY (uploaded_at DESC);
```

---

# Why Two Tables?

Cassandra is query-driven.

We support:

1. Fetch photo by photo_id
2. Fetch photos by user_id

Cassandra tables are designed around query patterns.

---

# API Endpoints

## Create Photo

```http
POST /api/photos
```

### Request

```json
{
  "userId": "user-101",
  "title": "Ocean View",
  "url": "https://cdn.demo.com/ocean.jpg"
}
```

---

## Get Photo By Id

```http
GET /api/photos/{photoId}
```

---

## Get Photos By User

```http
GET /api/photos/user/{userId}
```

---

# Structured Logging

Serilog is configured with:

- Correlation IDs
- Environment enrichment
- Thread enrichment

Example log:

```text
[14:05:11 INF]
[CorrelationId: abc-123]

Creating photo for user user-101
```

---

# Correlation IDs

Middleware automatically:

- reads `X-Correlation-ID`
- generates one if missing
- attaches it to logs
- returns it in response headers

Example:

```http
X-Correlation-ID: abc-123
```

---

# Health Checks

Endpoint:

```http
GET /health
```

Checks:

- Cassandra connectivity

---

# Startup Validation

Application validates Cassandra during startup:

- retries until Cassandra ready
- validates connectivity
- creates tables if needed

---

# Important Cassandra Concepts

## Query-Based Modeling

Cassandra tables are modeled around:

- read patterns
- partition keys
- clustering keys

NOT normalized relational design.

---

## Partition Key

```text
user_id
```

Used to distribute data across cluster nodes.

---

## Clustering Column

```text
uploaded_at
```

Used for sorting within partition.

---

# Production Considerations

Future improvements planned:

- OpenTelemetry tracing
- React frontend
- Cassandra paging
- Polly resilience policies
- Dockerized API
- Distributed tracing
- Grafana dashboards
- JWT authentication
- Rate limiting
- CQRS
- Kafka integration
- Background processing

---

# Common Issues

## Cassandra Startup Delay

Cassandra startup may take:

```text
20-60 seconds
```

especially first launch.

This is expected.

---

## NoHostAvailableException

Usually means:

- Cassandra not ready yet
- wrong contact points
- incorrect datacenter
- container not running

---

# Useful Docker Commands

## Check Running Containers

```bash
docker ps
```

---

## View Cassandra Logs

```bash
docker logs <container-id>
```

---

## Remove Container

```bash
docker rm -f <container-id>
```

---

# Learning Objectives

This project demonstrates:

- distributed system fundamentals
- Cassandra data modeling
- scalable API architecture
- structured logging
- observability
- startup orchestration
- service resilience
- production backend patterns

---

# Future Roadmap

- React frontend
- Pagination support
- OpenTelemetry tracing
- Grafana dashboards
- Authentication & Authorization
- File uploads
- S3 integration
- CDN support
- Kubernetes deployment
- CI/CD pipeline

---

# License

MIT