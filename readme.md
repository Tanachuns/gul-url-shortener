
# URL Shortener

desc

## Features
requirements

## Tech Stack
stackslist


## How to Run & Test


db ef:
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
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



//TODO
1. Create a short link — A user submits a long URL and receives a short link in return. Optionally allow /
a custom alias. Validate that the input is a proper URL./
2. View access statistics — For each link, the user can see usage stats — at minimum the total number
of times the short link has been visited (clicks), and ideally when it was created and last accessed./
3. Disable or delete a link — A user can disable a link (it stops redirecting but is kept for records) or
delete it entirely. A visit to a disabled/deleted link should not redirect./
4. Platform-specific destination — A short link can resolve to a different destination based on the
visitor's platform — e.g. iOS, Android, or a default — decided at redirect time.
https://gul.fy/HsQy5
Android → https://download.gulf.co.th/android.apk
iPhone  → https://download.gulf.co.th/iphone.ipa
5. Pluggable short-code generation /

