
#URL Shortener

desc

## Features
requirements

## Tech Stack
stackslist


## How to Run & Test
---
### Prerequisites
---
### Backend Setup (.NET)

---
### Frontend Setup (React + Vite)

---
### Running Tests

---
## API Contract Summary
Examples:

Core REST endpoints exposed by the .NET backend API:

| Method | Endpoint | Description |
| --- | --- | --- |
| `POST` | `/api/links` | Create a short link (payload: `defaultUrl`, `iosUrl`, `androidUrl`, `customAlias`). |
| `GET` | `/api/links` | Retrieve all created links and aggregated statistics. |
| `GET` | `/{code}` | Redirects visitor to target destination (evaluates platform User-Agent, updates click stats). |
| `PATCH` | `/api/links/{id}/toggle` | Enable or disable a short link. |
| `DELETE` | `/api/links/{id}` | Soft or hard delete a short link. |

---

## Key Design Decisions
how i  built this

