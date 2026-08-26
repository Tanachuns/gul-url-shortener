# URL Shortener
A link shortener built around the five capabilities below — the first three form the core user journey

## Features
1. **Create a short link** — A user submits a long URL and receives a short link in return. Optionally allow a custom alias. Validate that the input is a proper URL. 
2. **View access statistics** — For each link, the user can see usage stats — at minimum the total number of times the short link has been visited (clicks), and ideally when it was created and last accessed. 
3.  **Disable or delete a link** — A user can disable a link (it stops redirecting but is kept for records) or delete it entirely. A visit to a disabled/deleted link should not redirect. 
4.  **Platform-specific destination** — A short link can resolve to a different destination based on the visitor's platform — e.g. iOS, Android, or a default — decided at redirect time. 
5.  **Pluggable short-code generation** — Support more than one way to produce the short code — for example an auto-generated code and a user-supplied custom alias. When a visitor opens a short link, the service should redirect them to the original URL and increment the access count — unless the link is disabled or deleted.
## Tech Stack
**Backend:** .NET 10 MVC (C#)
**Frontend:** Vite (React Router)
**Database:** Sqlite
  
## Prerequisites

-   **.NET 10.0 SDK** (or later)
    
-   **Node.js** (v24.15.0) & **npm** (12.0.2)
    
-   **Git**

## Installation & Service Configuration

### 1. Clone Repository
```
git clone https://github.com/your-username/url-shortener.git
cd url-shortener

```

### 2. Backend Service Setup (.NET API)

1.  Restore NuGet dependencies and build:
     
    ```
    cd server/UrlShortener/UrlShortener
    dotnet restore
    dotnet build
    ```
    
2.  Apply EF Core migrations to initialize the SQLite database (`linkshortener.db`):    
    ```
    dotnet ef database update
    ```
    
3.  Configure environment settings in `appsettings.Development.json`:
    
    JSON
    
    ```
	{
		"Logging": {
			"LogLevel": {
				"Default": "Information",
				"Microsoft.AspNetCore": "Warning"
			}
		},
		"baseUrl": "https://localhost:7219",
		"ConnectionStrings": {
			"sqlite": "Data Source=C:\\Users\\User\\AppData\\Local\\linkshortener.db"
		}
	}
    
    ```
    

### 3. Frontend Service Setup (React + Vite)

1.  Install npm dependencies:
```
    cd ../../frontend
    npm install
    
```
    
2.  Configure environment variables in `.env.development`:
```
	VITE_API_BASE_URL=http://localhost:5243
```
    

## Testing & Service Verification

### 1. Automated Unit Tests

Run the test suite to verify URL validation logic, base-62 encoding, and service handlers:

Bash

```
cd server/UrlShortener/TestUrlShorter
dotnet test --verbosity normal
```


## API Contract Summary

Core REST endpoints exposed by the .NET backend API:

| Method | Endpoint | Description |
| --- | --- | --- |
| `POST` | `/api/links` | Create a short link (payload: `defaultUrl`, `iosUrl`, `androidUrl`, `customAlias`). |
| `GET` | `/api/links` | Retrieve all created links and aggregated statistics. |
| `GET` | `/api/links/{urlCode}` | Retrieve Single created link and aggregated statistics. |
| `GET` | `/{urlCode}` | Redirects visitor to target destination (evaluates platform User-Agent, updates click stats). |
| `PATCH` | `/api/links/{urlCode}` | Actived a short link. |
| `DELETE` | `/api/links/{urlCode}` | Soft delete (deactived) a short link. |

---
## Challenges & Next Steps

- My Previous job is usually works with backend so it a bit slow for frontend in design(states, data visualization and some react stuffs).
- After I did this project, I found that the url shortener detail is complex and interesting expesially in architecture and request load.

- Logs, I didn't implement logs and any mornitoring stuff but I think if it will go to prod. it must have logs.
- Url Code Generator, It currently use id to encode to base62 but we have custom alias that means it can duplicate.
- Microservices, I think it should split into 3 services: Redirect Service, Create  Short Url Service and Analytic Service  because if ew have a lot of request, It should be mostly on Redirect Service that we can only scale it.




**AI Session:** https://share.gemini.google/9tN5dct2l2mq